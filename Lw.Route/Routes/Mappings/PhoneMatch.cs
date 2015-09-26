/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配phone结果
    /// </summary>
    [UrlNameMap("phone")]
    class PhoneMatch : RegexMappingBase
    {
        protected override string RegexString=>@"^(\d{3}-\d{8}|\d{4}-\d{7}|\d{4}-\d{8})$";
    }
}