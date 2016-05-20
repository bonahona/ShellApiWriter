using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Driver
{
    public class TestDerivedClass : TestBaseClass
    {
        public String ThisWillDoSomething()
        {
            return "";
        }

        public override void ThisIsAlsoATest()
        {
            base.ThisIsAlsoATest();
        }
    }
}
