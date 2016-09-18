using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Domain
{
    public class Account
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Email { get; set; }


        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid Salt { get; set; }
    }
}
