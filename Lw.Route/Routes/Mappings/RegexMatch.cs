/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System;
using System.Text.RegularExpressions;

namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 自定义正则匹配结果
    /// <para>手动添加注册</para>
    /// </summary>
    class RegexMatch : MappingBase
    {
        /// <summary>
        /// 自定义正则匹配结果
        /// </summary>
        /// <param name="regexString">正则表达式字符串</param>
        public RegexMatch(string regexString)
        {
            this.regexString = regexString;
            this.regex = new Regex(regexString, RegexOptions.IgnoreCase|RegexOptions.Compiled);
        }
        /// <summary>
        /// 获取指定值的存储键
        /// </summary>
        public string GetKeyName => regexString;
        readonly Regex regex;
        readonly string regexString;
        /// <summary>
        /// 测试是否匹配
        /// </summary>
        /// <param name="urlPart"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool Test(string urlPart, out object result)
        {
            if (string.IsNullOrEmpty(urlPart))
            {
                result = DefaultValue;
                return IsNullAble;
            }
            var match = regex.Match(urlPart);
            if (match.Success)
            {
                result = match.Value;
                return true;
            }
            else
                result = string.Empty;
            return false;
        }
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public override bool IsNullAble => regex.IsMatch(string.Empty);
    }
}