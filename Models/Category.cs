using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category
    {
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string TimeCreated { get; set; }
        public string TimeUpdated { get; set; }


        public Category()
        {
            this.CategoryId = Guid.NewGuid().ToString();
            this.TimeCreated = DateTime.Now.ToString("ddd, dd MMM yyyy");
            this.TimeUpdated = DateTime.Now.ToString("ddd, dd MMM yyyy");
        }

    }
}
