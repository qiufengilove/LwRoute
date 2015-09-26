/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using Lw.Route.Routes;
using Lw.Route.Routes.Mappings;
using System.Threading;

namespace Lw.Route
{
    public class RouteHelper
    {
        /// <summary>
        /// 匹配的路由处理函数
        /// <para>包括初始化的和后期添加的</para>
        /// </summary>
        internal static readonly Dictionary<string, IRouteMatch> RouteMatchFuncs = new Dictionary<string, IRouteMatch>(StringComparer.OrdinalIgnoreCase);
        const string DefaultRouteMatchName = "Lw.Route.Routes.Mappings";
        static List<UrlManager> Routes = new List<UrlManager>();
        static RouteHelper()
        {
            var assembly = typeof(RouteHelper).Assembly;
            var nameType = typeof(UrlNameMapAttribute);
            var interfaceType = typeof(IRouteMatch);
            foreach (var type in assembly.GetTypes())
            {
                if (type.Namespace != DefaultRouteMatchName || type.IsAbstract || !interfaceType.IsAssignableFrom(type))
                    continue;
                var attribute = type.GetCustomAttributes(nameType, false).FirstOrDefault() as UrlNameMapAttribute;
                if (attribute != null)
                {
                    RouteMatchFuncs[attribute.GuidName] = Activator.CreateInstance(type) as IRouteMatch;
                }
            }
        }
        /// <summary>
        /// 默认域名下的其他页面
        /// </summary>
        static List<UrlManager> RouteMappings;
        /// <summary>
        /// 返回null表示首页
        /// </summary>
        /// <param name="url"></param>
        /// <param name="clearUrl">返回干净的Url</param>
        /// <returns></returns>
        static string[] GetUrlSplit(string url, out string clearUrl)
        {
            if (url == "/")
            {
                clearUrl = url;
                return null;
            }
            if (url.IndexOf('/') == 0)
                url = url.Substring(1);
            int lastIndex = url.Length - 1;
            if (url.LastIndexOf('/') == lastIndex)
                url = url.Substring(0, lastIndex);
            clearUrl = url;
            return url.Split('/');
        }
        /// <summary>
        /// 注册路由
        /// <para>约定的规则为 name:rulename或者name:length(最小长度,最大长度)或者name:regex(正则表达式)</para>
        /// </summary>
        /// <param name="url">比如:/n-admin/{action}/{id:int?}</param>
        /// <param name="insertTo"></param>
        public static void RegistRoute(string url, int insertTo = -1)
        {
            RegistRoute(Tuple.Create(url,insertTo));
        }
        /// <summary>
        /// 注册路由
        /// <para>约定的规则为 name:rulename或者name:length(最小长度,最大长度)或者name:regex(正则表达式)</para>
        /// </summary>
        /// <param name="urlRules">比如:/n-admin/{action}/{id:int?}</param>
        public static void RegistRoute(params Tuple<string,int>[] urlRules)
        {
            if (urlRules == null || urlRules.Length < 1)
                return;
            List<UrlManager> newList = new List<UrlManager>();
            List<UrlManager> tempList = RouteMappings;
            foreach (var tuple in urlRules)
            {
                //第一个为域名参数，如果以~/或者/开头，说明使用默认的域名
                //主机部分(做二级域名)
                var url = tuple.Item1;
                if (string.IsNullOrEmpty(url))
                    continue;
                string clearUrl;
                var rules = GetUrlSplit(url, out clearUrl);
                if (rules == null)
                {
                    //首页/
                    return;
                }
                if (tempList != null)
                {
                    foreach (var manage in tempList)
                    {
                        if (manage.UrlRule.Equals(clearUrl, StringComparison.OrdinalIgnoreCase))
                        {
                            throw new Exception("已经包含此路由名称");
                        }
                        newList.Add(manage);
                    }
                }
                int insertTo = tuple.Item2;
                if (insertTo < 0 || insertTo >= newList.Count)
                {
                    newList.Add(new UrlManager(clearUrl, rules));
                }
                else
                {
                    newList.Insert(insertTo, new UrlManager(clearUrl, rules));
                }
            }
            Interlocked.Exchange(ref RouteMappings, newList);
        }
        /// <summary>
        /// 匹配Url并得到路由参数
        /// </summary>
        /// <param name="testUrl"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        public static bool IsMatch(string testUrl,out IDictionary<string,object> paramter)
        {
            string clearUrl;
            var urlSplits = GetUrlSplit(testUrl,out clearUrl);
            if (urlSplits==null)
            {
                //首页自行处理
                paramter = null;
                return false;
            }
            IDictionary<string, object> dict = new Dictionary<string, object>();
            if (RouteMappings != null)
            {
                foreach (var match in RouteMappings)
                {
                    dict.Clear();
                    if (match.IsMatch(urlSplits, ref dict))
                    {
                        paramter = dict;
                        return true;
                    }
                }
            }
            paramter = null;
            return false;
        }
    }
}