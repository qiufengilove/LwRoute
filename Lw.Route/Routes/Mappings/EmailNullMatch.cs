/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配email?结果
    /// </summary>
    [UrlNameMap("email?")]
    class EmailNullMatch : EmailMatch
    {
        /// <summary>
        /// 默认值
        /// </summary>
        protected override object DefaultValue => string.Empty;
        public override bool IsNullAble => true;
    }
}