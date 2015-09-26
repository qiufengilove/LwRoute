/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配mac?结果
    /// <para>物理网卡地址</para>
    /// </summary>
    [UrlNameMap("mac?")]
    class MacNullMatch:MacMatch
    {
        public override bool IsNullAble => true;
    }
}