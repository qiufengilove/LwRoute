/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配tel?结果
    /// </summary>
    [UrlNameMap("tel?")]
    class TelNullMatch : TelMatch
    {
        //protected override bool IfNullResult => true;
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public override bool IsNullAble => true;
    }
}