/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配tel结果
    /// </summary>
    [UrlNameMap("tel")]
    class TelMatch : RegexMappingBase
    {
        protected override string RegexString=>@"^(\d{3}-\d{8}|\d{4}-\d{7}|\d{4}-\d{8}|1[3458]\d{9})$";
    }
}