using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1shop
{
    class AmountDiscount : OneItemDiscount
    {
        public double multiplier { get; set; }

        public AmountDiscount(Product item, double multiplier, int neededValue, bool membership) : base(item, neededValue, membership)
        {
            this.multiplier = multiplier;
        }
        public override int calculateDiscount(string cart)
        {
            if(itemCheck(cart) && neededCheck(cart) && membershipNeeded(cart))
            {
                //number of items in cart * discount * price
                return (int)Math.Round(inCartValue(cart) * (1 - multiplier) * item.price);
            }
            return 0; // no discount
        }
    }
}
