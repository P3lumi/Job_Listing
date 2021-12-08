using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Industry:BaseEntity
    {
        public string Name { get; set; }
        public List<Job> Jobs = new List<Job>();

        public Industry()
        {
            Jobs = new List<Job>();
        }

    }
}
