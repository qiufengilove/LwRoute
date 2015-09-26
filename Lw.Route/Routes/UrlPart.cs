/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using Lw.Route.Routes.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lw.Route.Routes
{
    /// <summary>
    /// 表示/分隔的每一个Url部分
    /// </summary>
    public class UrlPart
    {
        /// <summary>
        /// 表示/分隔的每一个Url部分
        /// </summary>
        /// <param name="urlRule"></param>
        public UrlPart(string urlRule)
        {
            UrlRule = urlRule.Trim();
            KeyAndIsConst = new List<RuleClass>();
            BuildRules();
        }
        private void BuildRules()
        {
            if (UrlRule.IndexOf('{') < 0)
            {
                KeyAndIsConst.Add(new RuleClass { ConstString = UrlRule });
                return;
            }
            //约定的规则为 name:rulename或者name:length(最小长度,最大长度)或者name:正则表达式
            //n-{name:email}
            var tempFuncDict =RouteHelper.RouteMatchFuncs;
            lock (tempFuncDict)
            {
                int start = UrlRule.IndexOf('{');
                int end = UrlRule.IndexOf('}');
                string tempUrl = UrlRule;
                _isNullAble = true;
                while (start >= 0 && end > 0)
                {
                    if (start > 0)
                    {
                        KeyAndIsConst.Add(new RuleClass { ConstString = tempUrl.Substring(0, start) });
                        _isNullAble = false;
                    }
                    string temp1 = tempUrl.Substring(start + 1, end - start - 1);
                    tempUrl = tempUrl.Substring(start + temp1.Length + 2).Trim();
                    //带规则应该用:分隔
                    //比如{key}不带规则的
                    if (temp1.IndexOf(':') < 0)
                    {
                        KeyAndIsConst.Add(new RuleClass { ConstString = temp1, IsOnlyKey = true, IsRule = true });
                        _isNullAble = false;
                    }
                    else
                    {
                        var tempStrs = temp1.Split(':');
                        var tempRuleClass = new RuleClass { ConstString = tempStrs[0], IsRule = true, RuleKey = tempStrs[1] };
                        IRouteMatch temp;
                        //直接作为键名
                        if (tempFuncDict.TryGetValue(tempRuleClass.RuleKey, out temp))
                        {
                            KeyAndIsConst.Add(tempRuleClass);
                            if (_isNullAble)
                                _isNullAble = temp.IsNullAble;
                        }
                        else
                        {
                            //分开是length和正则的情况
                            var indexLength = tempRuleClass.RuleKey.IndexOf("length(", StringComparison.Ordinal);
                            if (indexLength >= 0)
                            {
                                var tempLengths = tempRuleClass.RuleKey.Substring(indexLength + 7);
                                int indexLengthEnd = tempLengths.IndexOf(')');
                                if (indexLengthEnd > 0)
                                    tempLengths = tempLengths.Substring(0, indexLengthEnd);
                                var numbers = tempLengths.Split(',');
                                var tempRoute1 = new LengthMatch(numbers[0], numbers.Length > 1 ? numbers[1] : null);
                                tempRuleClass.RuleKey = tempRoute1.GetKeyName;
                                tempFuncDict[tempRoute1.GetKeyName] = tempRoute1;
                                KeyAndIsConst.Add(tempRuleClass);
                                if (_isNullAble)
                                    _isNullAble = tempRoute1.IsNullAble;
                            }
                            else
                            {
                                var tempRoute = new RegexMatch(tempRuleClass.RuleKey);
                                tempFuncDict[tempRoute.GetKeyName] = tempRoute;
                                tempRuleClass.RuleKey = tempRoute.GetKeyName;
                                KeyAndIsConst.Add(tempRuleClass);
                                if (_isNullAble)
                                    _isNullAble = tempRoute.IsNullAble;
                            }
                        }
                    }
                    if (tempUrl.Length == 0)
                        break;
                    _isNullAble = false;
                    start = tempUrl.IndexOf('{');
                    end = tempUrl.IndexOf('}');
                    //后面不再带规则
                    if(start<0)
                    {
                        KeyAndIsConst.Add(new RuleClass { ConstString = tempUrl });
                        break;
                    }
                    //验证规则
                    if(start==0)
                        throw new Exception("Url规则设定错误，两个自定义规则不能连接在一起");
                }
            }
        }
        readonly List<RuleClass> KeyAndIsConst;
        /// <summary>
        /// 表示/分隔的每一个Url部分
        /// </summary>
        readonly string UrlRule;
        /// <summary>
        /// 匹配/分隔的Url小片段
        /// </summary>
        /// <param name="urlPart"></param>
        /// <param name="resultRouteData"></param>
        /// <returns></returns>
        public bool IsMatch(string urlPart,ref IDictionary<string,object> resultRouteData)
        {
            for(int i=0;i<KeyAndIsConst.Count;i++)
            {
                if (!KeyAndIsConst[i].IsMatch(ref urlPart,(i < KeyAndIsConst.Count - 1 ? KeyAndIsConst[i + 1] : null), ref resultRouteData))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 创建Url
        /// <para>用于反射获取菜单</para>
        /// </summary>
        /// <param name="routeParamters"></param>
        /// <param name="builder"></param>
        public void BuildUrl(dynamic routeParamters, StringBuilder builder)
        {
            foreach(var rule in KeyAndIsConst)
            {
                builder.Append(rule.BuildUrl(routeParamters));
            }
        }
        bool _isNullAble;
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public bool IsNullAble => _isNullAble;
        /// <summary>
        /// 规则存储类
        /// </summary>
        class RuleClass
        {
            /// <summary>
            /// 是否有规则
            /// </summary>
            public bool IsRule { get; set; }
            /// <summary>
            /// 是否是规则，并且是否是规则的键名(否则就是定值规则)
            /// <para>比如{key}不带规则的，主要作为创建Url时的键名</para>
            /// </summary>
            public bool IsOnlyKey { get; set; }
            string _constString;
            /// <summary>
            /// 规则名称或者是指定值字符串
            /// </summary>
            public string ConstString { get { return _constString; } set { _constString = value?.Trim(); } }
            string _ruleKey;
            /// <summary>
            /// 规则键名
            /// </summary>
            public string RuleKey { get { return _ruleKey; } set { _ruleKey = value?.Trim(); } }
            /// <summary>
            /// 匹配/分隔的Url小片段
            /// </summary>
            /// <param name="urlPart"></param>
            /// <param name="next"></param>
            /// <param name="resultRouteData"></param>
            /// <returns></returns>
            public bool IsMatch(ref string urlPart, RuleClass next, ref IDictionary<string, object> resultRouteData)
            {
                //固定的字符串，不操作路由参数
                if (!IsRule)
                {
                    if (urlPart.Length == 0)
                        return false;
                    if (urlPart.IndexOf(ConstString, StringComparison.OrdinalIgnoreCase) != 0)
                        return false;
                    //如果是最后一个规则
                    if (next == null)
                    {
                        //固定的字符串长度和urlPart必须一样
                        return ConstString.Length == urlPart.Length;
                    }
                    //否则处理Url参数准备下一次匹配
                    urlPart = urlPart.Substring(ConstString.Length);
                    return urlPart.Length > 0;
                }
                //是规则，仅作为路由变量的键名
                if (IsOnlyKey)
                {
                    //如果是最后一个规则
                    if (next == null)
                    {
                        resultRouteData[ConstString] = urlPart;
                        return true;
                    }
                    if (urlPart.Length == 0)
                        return false;
                    //两个规则不能在一起(因为转换时已经检验，此种情况不会发生)
                    //if (next.IsRule)
                    //    return false;
                    //根据下一个规则界定边界
                    int indexStart = urlPart.IndexOf(next.ConstString, StringComparison.OrdinalIgnoreCase);
                    //下一个规则匹配失败
                    if (indexStart < 0)
                        return false;
                    string tempData = null;
                    if (indexStart == 0)
                    {
                        //跳过这个，如果后面不能匹配，返回false
                        int indexStart1 = urlPart.Substring(next.ConstString.Length).IndexOf(next.ConstString, StringComparison.OrdinalIgnoreCase);
                        if (indexStart1 < 0)
                            return false;
                        tempData = urlPart.Substring(0, next.ConstString.Length + indexStart1);
                    }
                    else
                        tempData = urlPart.Substring(0, indexStart);
                    resultRouteData[ConstString] = tempData;
                    urlPart = urlPart.Substring(tempData.Length);
                    return true;
                }
                //不是作为键名，有检验规则
                IRouteMatch tempMatch;
                if (!RouteHelper.RouteMatchFuncs.TryGetValue(RuleKey, out tempMatch))
                {
                    throw new Exception("检验规则:" + RuleKey + " 丢失，请检查原因");
                }
                object routeData;
                //如果是最后一个规则
                if (next == null)
                {
                    if (!tempMatch.Test(urlPart, out routeData))
                        return false;
                    resultRouteData[ConstString] = routeData;
                    //urlPart = string.Empty;
                    return true;
                }
                //否则需要处理Url
                //两个规则不能在一起(因为转换时已经检验，此种情况不会发生)
                //if (next.IsRule)
                //    return false;
                //根据下一个规则界定边界
                int indexStart2 = urlPart.IndexOf(next.ConstString, StringComparison.OrdinalIgnoreCase);
                if (indexStart2 < 0)
                    return false;
                //获取真正用于匹配的部分
                string tempUrlPart = null;
                if (indexStart2 == 0)
                {
                    //跳过这个，如果后面不能匹配，返回false
                    int indexStart1 = urlPart.Substring(next.ConstString.Length).IndexOf(next.ConstString, StringComparison.OrdinalIgnoreCase);
                    if (indexStart1 < 0)
                        return false;
                    tempUrlPart = urlPart.Substring(0, next.ConstString.Length + indexStart1);
                }
                else
                    tempUrlPart = urlPart.Substring(0, indexStart2);
                if (!tempMatch.Test(tempUrlPart, out routeData))
                    return false;
                resultRouteData[ConstString] = routeData;
                urlPart = urlPart.Substring(tempUrlPart.Length);
                return true;
            }
            /// <summary>
            /// 根据路由参数创建Url
            /// </summary>
            /// <param name="paramter"></param>
            /// <returns></returns>
            public string BuildUrl(dynamic paramter)
            {
                if (!IsRule)
                    return this.ConstString;
                object routeData = paramter[ConstString];
                if (IsOnlyKey)
                {
                    return routeData == null ? string.Empty : routeData.ToString();
                }
                //不是作为键名，有检验规则
                IRouteMatch tempMatch;
                if (!RouteHelper.RouteMatchFuncs.TryGetValue(RuleKey, out tempMatch))
                {
                    return string.Empty;
                }
                object result;
                if (!tempMatch.Test((routeData == null ? string.Empty : routeData.ToString()), out result))
                    return string.Empty;
                return result.ToString();
            }
        }
    }
}