using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spices.Models.ViewModels
{
    public class OrderDetailsCard
    {
        public List<ShoppingCard> ListCard { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
