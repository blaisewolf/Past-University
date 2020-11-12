using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1shop
{
    class Coupon
    {
        public string id;
        public double multiplier;

        public Coupon(string id, double multiplier)
        {
            this.id = id;
            this.multiplier = multiplier;
        }
    }
}
