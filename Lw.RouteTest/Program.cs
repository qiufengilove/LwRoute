using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;
using Lw.Route;
using System.Diagnostics;

namespace Lw.RouteTest
{
    class Program
    {
        [Debuggable]
        static void Main(string[] args)
        {
            //匹配自定义规则
            RouteHelper.RegistRoute("/u-test/{action}-{id:int}/{name:long}");
            //可空的Email
            RouteHelper.RegistRoute("/d-test/{action}/{name:email?}");
            //可空的QQ
            RouteHelper.RegistRoute("/n-test/{action}/{name:qq?}");
            //可空的Id
            RouteHelper.RegistRoute("/u-test/{action}/{id:int?}");
            //自定义正则表达式(只接受字母)
            RouteHelper.RegistRoute("/u-regex/{action}/{name:^[a-zA-Z]+$}");
            int count = 1000000;
            //var testUrl = "/u-test/mytest/3000";
            var testUrl = "/u-regex/mytest/afdAW";
            IDictionary<string, object> dict = null;
            var timecount = System.Diagnostics.Stopwatch.StartNew();
            bool result=false;
            for (int i=0;i< count; i++)
            {
                result=RouteHelper.IsMatch(testUrl, out dict);
            }
            timecount.Stop();
            Console.WriteLine("测试"+ count +"次匹配，消耗时间:"+ timecount.ElapsedMilliseconds+"毫秒");
            Debug.Assert(dict != null && dict.ContainsKey("action") && dict["action"].ToString() == "mytest", "测试检索action失败");
            //Debug.Assert(dict != null && dict.ContainsKey("name") && dict["name"].ToString() == "3000", "测试检索Id失败");
            
            Console.WriteLine(result.ToString());
            //Console.WriteLine(dict["action"]+"                "+dict["id"]);
        }
    }
}
