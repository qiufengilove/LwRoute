/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System.Text.RegularExpressions;

namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 正则表达式匹配基类
    /// </summary>
    abstract class RegexMappingBase : MappingBase
    {
        /// <summary>
        /// 正则表达式匹配基类
        /// </summary>
        public RegexMappingBase()
        {
            regex = new Regex(RegexString, RegexOptions.Compiled);
        }
        readonly Regex regex;
        /// <summary>
        /// 正则表达式字符串
        /// </summary>
        protected abstract string RegexString { get; }
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
    }
}