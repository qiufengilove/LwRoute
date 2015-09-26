/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配汉字结果
    /// </summary>
    [UrlNameMap("chinese")]
    class ChineseMatch : RegexMappingBase
    {
        protected override string RegexString=> @"^[^\x00-\x80\uFE30-\uFFA0]+$";
    }
}