using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        static void Main(string[] args)
        {
            bool bbbb = client.CheckNameEmail("김만aa규aaa", "aksrb1030@naver.com");
            bool a = client.CheckNamePhone("김만규", "01041703622");
            Console.WriteLine(bbbb);
            Console.WriteLine(a);
        }
    }
}
