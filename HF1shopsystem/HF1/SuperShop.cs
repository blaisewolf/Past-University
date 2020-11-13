using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1
{
    class SuperShop
    {
        public string id;
        public int points = 0;

        public SuperShop(string id) 
        {
            this.id = id;
        }
        private void AfterBuyPoints(int price)
        {
            if((price - points) >= 0)
            {
                points = 0;
            }
            else
            {
                points = Math.Abs(price - points);
            }
        } 
        private void CalculatePoints(int price)
        {
            points += (int)(price * 0.01);
        }
        public int SuperShopProcess(string cart, int price)
        {
            int pointPay = 0;
            if (paysWithSuperShop(cart))
            {
                pointPay = points;
                AfterBuyPoints(price);
            }
            CalculatePoints(price);
            return pointPay;
        }
        private bool paysWithSuperShop(string cart)
        {
            return cart.Contains("p");
        }
    }
}
