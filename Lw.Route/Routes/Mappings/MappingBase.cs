/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配Url基类
    /// </summary>
    abstract class MappingBase : IRouteMatch
    {
        /// <summary>
        /// 默认值
        /// </summary>
        protected virtual object DefaultValue => null;
        /// <summary>
        /// 如果为空返回结果
        /// </summary>
        //protected virtual bool IfNullResult => false;
        /// <summary>
        /// 测试是否匹配
        /// </summary>
        /// <param name="urlPart"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public abstract bool Test(string urlPart, out object result);
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public virtual bool IsNullAble => false;
    }
}