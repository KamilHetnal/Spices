using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spices.Models.ViewModels
{
    public class OrderListViewModel
    {
        public IList<OrderDetailsVeiwModel> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
