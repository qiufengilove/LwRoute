/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System;

namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配date结果
    /// <para>时间格式</para>
    /// </summary>
    [UrlNameMap("date")]
    class DateMatch : MappingBase
    {
        protected override object DefaultValue => DateTime.MinValue;
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
            if(urlPart.Length>10)
            {
                result = string.Empty;
                return false;
            }
            DateTime temp;
            if (DateTime.TryParse(urlPart, out temp))
            {
                result = temp;
                return true;
            }
            else
                result = string.Empty;
            return false;
        }
    }
}