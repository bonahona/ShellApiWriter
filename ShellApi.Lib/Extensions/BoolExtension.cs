using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellApi.Lib.Extensions
{
    public static class BoolExtension
    {
        public static int ToInt(this bool b)
        {
            if (b) {
                return 1;
            }else {
                return 0;
            }
        }
    }
}
