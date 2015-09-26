/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配postcode结果
    /// <para>大陆邮政编码</para>
    /// </summary>
    [UrlNameMap("postcode")]
    class PostcodeMatch : RegexMappingBase
    {
        protected override string RegexString=> @"^[1-9]\d{5}$";
    }
}