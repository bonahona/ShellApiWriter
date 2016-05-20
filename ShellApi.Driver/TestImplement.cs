using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Driver
{
    public class TestImplement : TestInterface
    {
        public void DoSomething(int a, int b)
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
