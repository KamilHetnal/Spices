﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spices.Models.ViewModels
{
    public class OrderDetailsVeiwModel
    {
        public OrderHeader OrderHeader { get; set;}
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
