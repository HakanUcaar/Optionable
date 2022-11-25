using Optionable.Sample.Attributies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Sample
{
    public class Customer
    {        
        public int Id { get; set; }
        [NotNull]
        public string Surname { get; set; }
        public string Forename { get; set; }
        public decimal Discount { get; set; }
        [NotNull]
        public string Address { get; set; }
    }
}
