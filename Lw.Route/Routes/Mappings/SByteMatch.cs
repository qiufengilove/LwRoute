/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配sbyte结果
    /// </summary>
    [UrlNameMap("sbyte")]
    class SByteMatch : MappingBase
    {
        protected override object DefaultValue => 0;
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
            sbyte temp;
            if (sbyte.TryParse(urlPart, out temp))
            {
                result = temp;
                return true;
            }
            else
                result = 0;
            return false;
        }
    }
}