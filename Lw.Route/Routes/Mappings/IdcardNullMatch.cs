/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配idcard?结果
    /// <para>大陆身份证号码</para>
    /// </summary>
    [UrlNameMap("idcard?")]
    class IdcardNullMatch : IdcardMatch
    {
        //protected override bool IfNullResult => true;
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public override bool IsNullAble => true;
    }
}