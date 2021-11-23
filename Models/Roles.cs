using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Roles
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public User user { get; set; }
    }
}
