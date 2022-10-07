using System;
using System.ComponentModel.DataAnnotations;

namespace CarService
{
    public class Bid
    {
        [Key]
        public int Id_Bid { get; set; }
        public string NumberBid { get; set; }

        public string LFP { get; set; }

        public string Brand { get; set; }
        
        public int Id_TypeWork { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public bool Succes { get; set; }

        public string Comment { get; set; }

        public virtual TypeWork Type { get; set; }
    }
}
