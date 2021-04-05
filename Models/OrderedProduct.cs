using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class OrderedProduct
    {
        [Key]
        [Column(Order = 0)]
        public int ProductId { get; set; }

        
        [Column(Order = 1)]
        public int CustomerOrderId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}

