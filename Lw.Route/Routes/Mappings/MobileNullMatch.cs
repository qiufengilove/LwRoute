/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配mobile?结果
    /// </summary>
    [UrlNameMap("mobile?")]
    class MobileNullMatch : MobileMatch
    {
        //protected override bool IfNullResult => true;
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public override bool IsNullAble => true;
    }
}