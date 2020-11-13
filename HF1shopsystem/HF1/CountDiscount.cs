using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1
{
    class CountDiscount : OneItemDiscounts
    {
        public int discountValue { get; set; }

        public CountDiscount(Product item, int discountValue, int neededValue, bool membership) : base(item, neededValue, membership)
        {
            this.discountValue = discountValue;
        }
        public override int calculateDiscount(string cart)
        {
            if (itemCheck(cart) && neededCheck(cart) && MembershipNeeded(cart))
            {
                //number of times the discount applies * number of ignored items * price
                return (inCartValue(cart) / neededValue) * (neededValue - discountValue) * item.price; 
            }
            return 0; //no discount
        }
    }
}
