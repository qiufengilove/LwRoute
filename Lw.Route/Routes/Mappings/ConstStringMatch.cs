/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
using System;

namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配指定值字符串结果
    /// <para>手动添加注册</para>
    /// </summary>
    class ConstStringMatch : MappingBase
    {
        /// <summary>
        /// 匹配指定值字符串结果
        /// </summary>
        /// <param name="constString"></param>
        public ConstStringMatch(string constString)
        {
            this.constString = constString?.ToLower();
        }
        /// <summary>
        /// 获取指定值的存储键
        /// </summary>
        public string GetKeyName => constString;
        readonly string constString;
        /// <summary>
        /// 测试是否匹配
        /// </summary>
        /// <param name="urlPart"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool Test(string urlPart, out object result)
        {
            if (string.IsNullOrEmpty(urlPart))
            {
                result = DefaultValue;
                return IsNullAble;
            }
            if (constString.Equals(urlPart,StringComparison.OrdinalIgnoreCase))
            {
                result = constString;
                return true;
            }
            else
                result = string.Empty;
            return false;
        }
    }
}