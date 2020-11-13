using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1
{
    class Shop
    {
        private Dictionary<char, Product> products;
        private List<DiscountBase> discounts;
        private Dictionary<string ,SuperShop> superShopMembers;
        private Dictionary<string, Coupon> coupons;

        public Shop()
        {
            products = new Dictionary<char, Product>();
            discounts = new List<DiscountBase>();
            superShopMembers = new Dictionary<string, SuperShop>();
            coupons = new Dictionary<string, Coupon>();
        }

        internal void RegisterProduct(char name, int price)
        {
            products.Add(name, new Product(name, price));
        }
        internal void RegisterCountDiscount(char name, int discount, int needed, bool membership)
        {
            discounts.Add(new CountDiscount(products[name], discount, needed, membership));
        }
        internal void RegisterAmountDiscount(char name, double multiplier, int needed, bool membership)
        {
            discounts.Add(new AmountDiscount(products[name], multiplier, needed, membership));
        }
        internal void RegisterComboDiscount(string combo, int price, bool membership)
        {
            discounts.Add(new ComboDiscounts(getSelectedProducts(combo), price, membership));
        }
        internal void RegisterSuperShopMember(string id)
        {
            superShopMembers.Add(id, new SuperShop(id));
        }
        internal void RegisterCoupon(string id, double multiplier)
        {
            coupons.Add(id, new Coupon(id, multiplier));
        }
        internal List<Product> getSelectedProducts(string items)
        {
            List<Product> selectedProducts = new List<Product>();
            foreach (char i in items)
            {
                selectedProducts.Add(products[i]);
            }
            return selectedProducts;
        }
        internal int GetPrice(string cart)
        {
            int price = (int)((calculatePrice(cart) - calculateDiscounts(cart)) * clubMemberShipValue(cart) * CouponValue(cart));
            price -= SuperShopValue(cart, price);
            return price;
        }
        internal int SuperShopValue(string cart, int price)
        {
            if (IsSuperShopMember(cart))
            {
                string id = GetIdNumber(cart, 'v');
                return superShopMembers[id].SuperShopProcess(cart, price);
            }
            return 0;
        }
        internal string GetIdSubstring(string cart, char splitter)
        {
            string[] substrings = cart.Split(splitter);
            return substrings[1]; // the second element will always contain the ID
        }
        internal string GetIdNumber(string cart, char splitter)
        {
            List<char> id = new List<char>();
            string cartWithV = GetIdSubstring(cart, splitter);
            foreach (char c in cartWithV)
            {
                if (char.IsDigit(c))
                {
                    id.Add(c);
                }
                else
                {
                    break; // The first characters of the string is always the ID(numbers).
                }
            }
            return string.Join("", id);
        }
        public double CouponValue(string cart)
        {
            if (!cart.Contains("k"))
            {
                return 1;
            }
            string id = GetIdNumber(cart, 'k');
            if (coupons.ContainsKey(id))
            {
                double multiplier = coupons[id].multiplier;
                coupons.Remove(id);
                return (multiplier > 0) ? multiplier : 1;
            }
            return 1; // multiplication by 1 doesnt change anything
        }
        internal bool IsSuperShopMember(string cart)
        {
            return cart.Contains("v");
        }
        internal int calculatePrice(string cart)
        {
            int price = 0;
            foreach (char c in cart)
            {
                try
                {
                    price += products[c].price;
                }
                catch
                {
                    continue;
                }
            }
            return price;
        }
        internal int calculateDiscounts(string cart)
        {
            int discountPrice = 0;
            foreach(var d in discounts)
            {
                discountPrice += d.calculateDiscount(cart);
            }
            return discountPrice;
        }
        internal double clubMemberShipValue(string cart)
        {
            return IsSuperShopMember(cart) ? 0.9 : 1; // multiplying by 1 doesn't change anything. (No ClubMemberShip) 
        }
    }
}
