/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System;

namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 用于标记类为用于Url匹配
    /// </summary>
    public class UrlNameMapAttribute:Attribute
    {
        /// <summary>
        /// 用于标记类为用于Url匹配
        /// </summary>
        /// <param name="guidName"></param>
        public UrlNameMapAttribute(string guidName)
        {
            this.GuidName = guidName;
        }
        /// <summary>
        /// 唯一Guid名称
        /// </summary>
        public string GuidName { get; set; }
    }
}