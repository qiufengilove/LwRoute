/// <summary>
/// 作者：龙之舞
/// 时间：2015-9-26
/// License Apache 2.0
/// </summary>
namespace Lw.Route.Routes.Mappings
{
    /// <summary>
    /// 匹配length结果
    /// <para>出于性能考虑，length严格限制为使用小写</para>
    /// </summary>
    class LengthMatch : MappingBase
    {
        const string ThisKeyName = "length({0},{1})";
        /// <summary>
        /// 设置最小和最大长度
        /// <para>用于注册路由时</para>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>返回实际的键值length({0},{1})</returns>
        public LengthMatch(string left,string right)
        {
            left = left.Trim();
            right = right.Trim();
            if (string.IsNullOrEmpty(left))
                this.leftInt = 0;
            else
                int.TryParse(left, out this.leftInt);
            if (string.IsNullOrEmpty(right))
                this.rightInt = 0;
            else
                int.TryParse(right, out this.rightInt);
            if (leftInt < 0)
                leftInt = 0;
            if (rightInt < 0)
                rightInt = 0;
            _keyName= string.Format(ThisKeyName, leftInt, rightInt);
        }
        readonly int leftInt;
        readonly int rightInt;
        string _keyName;
        /// <summary>
        /// 获取指定值的存储键
        /// </summary>
        public string GetKeyName=> _keyName;
        protected override object DefaultValue =>string.Empty;
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
            //小于最小长度
            if (leftInt > 0 && urlPart.Length < leftInt)
            {
                result = DefaultValue;
                return false;
            }
            //大于最大长度
            if (rightInt > 0 && urlPart.Length > rightInt)
            {
                result = DefaultValue;
                return false;
            }
            result = urlPart;
            return true;
        }
        /// <summary>
        /// 是否可忽略参数
        /// </summary>
        public override bool IsNullAble => leftInt == 0;
    }
}