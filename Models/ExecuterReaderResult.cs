using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class ExecuterReaderResult
    {

        public List<string> Fields { get; set; }
        public List<string> Values { get; set; }

        public ExecuterReaderResult()
        {
            Fields = new List<string>();
            Values = new List<string>();
        }

    }
}
