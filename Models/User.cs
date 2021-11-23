using System;

namespace Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }


        public User()
        {
            this.UserId = Guid.NewGuid().ToString();
        }
    }
}
