/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配short?结果
    /// </summary>
    [UrlNameMap("short?")]
    class ShortNullMatch : ShortMatch
    {
        /// <summary>
        /// 如果为空，返回结果
        /// </summary>
        //protected override bool IfNullResult => true;
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public override bool IsNullAble => true;
    }
}