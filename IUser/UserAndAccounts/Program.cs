using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAndAccounts
{
    class Program
    {
        static void Main(string[] args)
        {
            TestUser testUser = new TestUser();
            testUser.DoAsync().Wait();
        }
    }
}
