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
        static void Main(string[] args)
        {
            RouteHelper.RegistRoute("/u-test/{action}-{id:int}/{name:long}");
            RouteHelper.RegistRoute("/d-test/{action}/{name:email?}");
            RouteHelper.RegistRoute("/n-test/{action}/{name:qq?}");
            RouteHelper.RegistRoute("/u-test/{action}/{id:int?}");
            int count = 1000000;
            var testUrl = "/u-test/mytest/3000";
            IDictionary<string, object> dict = null;
            var timecount = System.Diagnostics.Stopwatch.StartNew();
            for (int i=0;i< count; i++)
            {
                RouteHelper.IsMatch(testUrl, out dict);
            }
            timecount.Stop();
            Console.WriteLine("测试"+ count +"次匹配，消耗时间:"+ timecount.ElapsedMilliseconds+"毫秒");
            Console.WriteLine(dict.Count);
            Debug.Assert(dict != null && dict.ContainsKey("action") && dict["action"].ToString() == "mytest", "测试检索action失败");
            Debug.Assert(dict != null && dict.ContainsKey("id") && dict["id"].ToString() == "3000", "测试检索Id失败");

            Console.WriteLine(dict["action"]+"                "+dict["id"]);
        }
    }
}
