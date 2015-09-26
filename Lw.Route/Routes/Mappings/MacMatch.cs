/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配mac结果
    /// <para>物理网卡地址</para>
    /// </summary>
    [UrlNameMap("mac")]
    class MacMatch : RegexMappingBase
    {
        protected override string RegexString=> @"^[A-Fa-f\d]{2}-[A-Fa-f\d]{2}-[A-Fa-f\d]{2}-[A-Fa-f\d]{2}-[A-Fa-f\d]{2}-[A-Fa-f\d]{2}$";
    }
}