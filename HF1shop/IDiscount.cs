﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1shop
{
    interface IDiscount
    {
        int calculateDiscount(string cart);
        bool itemCheck(string cart);
    }
}