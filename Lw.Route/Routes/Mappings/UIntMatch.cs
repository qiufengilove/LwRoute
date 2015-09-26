/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配uint结果
    /// </summary>
    [UrlNameMap("uint")]
    class UIntMatch : MappingBase
    {
        protected override object DefaultValue => 0U;
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
            uint re;
            if (uint.TryParse(urlPart, out re))
            {
                result = re;
                return true;
            }
            else
                result = 0U;
            return false;
        }
    }
}