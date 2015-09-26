/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配字母和数字结果
    /// </summary>
    [UrlNameMap("letters")]
    class LettersMatch : RegexMappingBase
    {
        protected override string RegexString=>"^[a-zA-Z0-9]+$";
    }
}