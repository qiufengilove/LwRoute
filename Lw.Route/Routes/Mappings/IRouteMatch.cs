/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 路由组件是否匹配的接口
    /// </summary>
    interface IRouteMatch
    {
        /// <summary>
        /// 测试是否匹配
        /// </summary>
        /// <param name="urlPart"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool Test(string urlPart, out object result);
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        bool IsNullAble { get; }
    }
}