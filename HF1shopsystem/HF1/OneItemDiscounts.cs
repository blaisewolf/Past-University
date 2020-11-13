using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1
{
    abstract class OneItemDiscounts : DiscountBase
    {
        public Product item { get; set; }
        public int neededValue { get; set; }
        public OneItemDiscounts(Product item, int neededValue, bool membership) : base(membership)
        {
            this.item = item;
            this.neededValue = neededValue;
        }

        protected virtual int inCartValue(string cart)
        {
            return cart.ToCharArray().Count(c => c == item.name);
        }

        public override bool itemCheck(string cart)
        {
            if (cart.Contains(item.name))
            {
                return true;
            }
            return false;
        }
        protected virtual bool neededCheck(string cart)
        {
            return (inCartValue(cart) >= neededValue) ? true : false;
        }
    }
}
