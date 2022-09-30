using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService
{
    public class TypeWork
    {
        [Key]
        public int Id_TypeWork { get; set; }

        public string Type { get; set; }
        
        public decimal Cost { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
