using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1shop
{
    abstract class DiscountBase : IDiscount
    {
        public bool membership;
        public DiscountBase(bool membership = false)
        {
            this.membership = membership;
        }
        public DiscountBase()
        {
            this.membership = false;
        }
        public abstract int calculateDiscount(string cart);
        public abstract bool itemCheck(string cart);
        protected bool membershipNeeded(string cart)
        {
            if(membership)
            {
                return cart.Contains("v"); // membership indicator
            }
            return true;
        }
    }
}
