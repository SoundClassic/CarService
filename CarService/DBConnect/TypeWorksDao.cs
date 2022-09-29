using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService
{
    public class TypeWorksDao
    {
        [Key]
        public int Id_TypeWork { get; set; }

        public string TypeWork { get; set; }
        
        public decimal Cost { get; set; }

        public virtual ICollection<BidDao> Bids { get; set; }
    }
}
