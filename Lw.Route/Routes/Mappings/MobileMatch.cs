/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配大陆电话号码结果
    /// </summary>
    [UrlNameMap("mobile")]
    class MobileMatch : RegexMappingBase
    {
        protected override string RegexString=>@"^1[3458]\d{9}$";
    }
}