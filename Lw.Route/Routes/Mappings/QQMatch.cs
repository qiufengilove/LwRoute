/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配qq结果
    /// </summary>
    [UrlNameMap("qq")]
    class QQMatch : RegexMappingBase
    {
        protected override string RegexString=> @"^[1-9]\d{4,}$";
    }
}