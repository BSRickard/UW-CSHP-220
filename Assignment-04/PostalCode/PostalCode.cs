using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PostalCodes
{
    class PostalCode
    {
        public string Region;
        public Regex  Regex;

        public PostalCode(string region, string pattern)
        {
            Region = region;
            Regex  = new Regex(pattern);
        }
    }
}
