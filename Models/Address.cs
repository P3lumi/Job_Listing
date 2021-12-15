using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Address
    {
        [Key]
        public string AppUserId { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public AppUser AppUser { get; set; }
    }
}