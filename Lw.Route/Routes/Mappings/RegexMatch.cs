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
    /// 自定义正则匹配结果(带默认值)
    /// <para>手动添加注册</para>
    /// </summary>
    class RegexMatch : MappingBase
    {
        /// <summary>
        /// 自定义正则匹配结果
        /// </summary>
        /// <param name="regexString">正则表达式字符串</param>
        /// <param name="defaultStr">默认字符串</param>
        public RegexMatch(string regexString, string defaultStr = null)
        {
            this.regexString = regexString;
            this.regex = new Regex(regexString, RegexOptions.Compiled);
            this._defaultStr = defaultStr;
            if (_isNullAble)
                _keyName = string.Format("{0}||{1}", regexString, _defaultStr);
            else
                _keyName = regexString;
            _isNullAble = (_defaultStr != null && _defaultStr.Length > 0);
        }
        readonly string _defaultStr;
        readonly string _keyName;
        /// <summary>
        /// 获取指定值的存储键
        /// </summary>
        public string GetKeyName => _keyName;
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
            if (urlPart == null || urlPart.Length == 0)
            {
                result = _defaultStr;
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
        bool _isNullAble;
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public override bool IsNullAble => _isNullAble;
    }
}