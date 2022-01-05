using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cv
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsMain { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
