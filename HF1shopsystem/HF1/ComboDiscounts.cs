using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1
{
    class ComboDiscounts : DiscountBase
    {
        public List<Product> items;
        public int discountPrice;

        public ComboDiscounts(List<Product> items, int discountPrice, bool membership) : base(membership)
        {
            this.items = items;
            this.discountPrice = discountPrice;
        }
        public override int calculateDiscount(string cart)
        {
            if(itemCheck(cart) && MembershipNeeded(cart))
            {
                return getOriginalPrice(items) - discountPrice;
            }
            return 0; // no discount
        }
        private int getOriginalPrice(List<Product> items)
        {
            return items.Select(i => i.price).Sum();
        }
        public override bool itemCheck(string cart)
        {
            foreach (Product i in items)
            {       
                if (!cart.Contains(i.name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
