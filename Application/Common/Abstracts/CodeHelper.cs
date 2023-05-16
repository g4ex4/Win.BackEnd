using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstracts
{
    public abstract class CodeHelper
    {
        public static int GetRandomCode(int length)
        {
            Random rnd = new Random();
            int up = (int)Math.Pow(10, length);
            int code = rnd.Next(up);
            if (length > 9)
            {
                throw new Exception("code should have length lower then 9");
            }
            return code;
        }
    }
}
