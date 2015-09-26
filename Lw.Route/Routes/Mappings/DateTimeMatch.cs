/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System;

namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配datetime结果
    /// </summary>
    [UrlNameMap("datetime")]
    class DateTimeMatch : MappingBase
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
            DateTime temp;
            if (DateTime.TryParse(urlPart, out temp))
            {
                result = temp;
                return true;
            }
            else
                result = DateTime.MinValue;
            return false;
        }
    }
}