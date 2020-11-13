using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1
{
    abstract class DiscountBase : IDiscount
    {
        protected bool membership;
        public DiscountBase(bool membership)
        {
            this.membership = membership;
        }
        public DiscountBase()
        {
            this.membership = false;
        }
        public abstract int calculateDiscount(string cart);

        public abstract bool itemCheck(string cart);
        protected bool MembershipNeeded(string cart)
        {
            if (membership)
            {
                return cart.Contains("v");
            }
            return true;
        }
    }
}
