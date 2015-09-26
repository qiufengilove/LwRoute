/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System;
using System.Collections.Generic;
using System.Text;

namespace Lw.Route.Routes
{
    /// <summary>
    /// Url路由和处理方法统一管理类
    /// </summary>
    class UrlManager
    {
        /// <summary>
        /// Url路由和处理方法统一管理类
        /// </summary>
        /// <param name="urlRule"></param>
        /// <param name="urlParts"></param>
        public UrlManager(string urlRule, string[] urlParts)
        {
            UrlRule = urlRule;
            UrlParts = new List<UrlPart>();
            //检查
            foreach(var part in urlParts)
            {
                UrlParts.Add(new UrlPart(part));
            }
            MinLength = UrlParts.Count;
            for (int i=urlParts.Length-1;i>=0;i--)
            {
                if (UrlParts[i].IsNullAble)
                    MinLength--;
                else
                    break;
            }
        }
        /// <summary>
        /// Url是否已经注册
        /// <para>键值</para>
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsUrlRegisted(string url)
        {
            return UrlRule.Equals(url,StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// Url分片最小长度
        /// <para>计算可忽略参数</para>
        /// </summary>
        int MinLength;
        /// <summary>
        /// 路由规则
        /// </summary>
        public readonly string UrlRule;
        public readonly List<UrlPart> UrlParts;
        /// <summary>
        /// 匹配/分隔的Url小片段
        /// </summary>
        /// <param name="urlParts"></param>
        /// <param name="resultRouteData"></param>
        /// <returns></returns>
        public bool IsMatch(string[] urlParts, ref IDictionary<string, object> resultRouteData)
        {
            if (urlParts.Length> UrlParts.Count || urlParts.Length < MinLength)
                return false;
            int urlLength = urlParts.Length;
            if (urlLength < UrlParts.Count)
            {
                int maxIndex = urlLength - 1;
                for (var i = 0; i < UrlParts.Count; i++)
                {
                    if (i > maxIndex)
                    {
                        if (!UrlParts[i].IsNullAble)
                            return false;
                        continue;
                    }
                    if (!UrlParts[i].IsMatch(urlParts[i], ref resultRouteData))
                        return false;
                }
            }
            else
            {
                for (var i = 0; i < UrlParts.Count; i++)
                {
                    if (!UrlParts[i].IsMatch(urlParts[i], ref resultRouteData))
                        return false;
                }
            }
            return true;
        }
        StringBuilder _builder;
        StringBuilder Builder => _builder ?? (_builder=new StringBuilder());
        /// <summary>
        /// 创建Url
        /// <para>用于反射获取菜单</para>
        /// </summary>
        /// <param name="routeParamters"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public string BuildUrl(dynamic routeParamters)
        {
            Builder.Clear();
            foreach (var part in UrlParts)
            {
                part.BuildUrl(routeParamters, Builder);
                Builder.Append("/");
            }
            return Builder.ToString();
        }
    }
}