using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HF1
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Shop s1;
        public UnitTest1()
        {
            s1 = new Shop();
        }
        [TestMethod]
        public void AddProductsAndGetPrice()
        {
            s1.RegisterProduct('A', 20);
            s1.RegisterProduct('B', 30);
            s1.RegisterProduct('C', 50);
            Assert.AreEqual(100, s1.GetPrice("ABC"));
        }
        [TestMethod]
        public void CheckClubMembershipTrue()
        {
            s1.RegisterProduct('A', 20);
            s1.RegisterProduct('B', 30);
            s1.RegisterProduct('C', 50);
            s1.RegisterSuperShopMember("1");
            Assert.AreEqual(100*0.9, s1.GetPrice("ABCv1"));
        }
        [TestMethod]
        public void CountDiscount()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('E', 50);
            s1.RegisterCountDiscount('A', 3,4, false);
            Assert.AreEqual(190, s1.GetPrice("AAAAAEEE")); // 5* A(10) = 50    3 * E(50) = 150    40+ 150 = 190
        }
        [TestMethod]
        public void AmountDiscount()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 100);
            s1.RegisterAmountDiscount('A',0.9,5, false); 
            Assert.AreEqual(154, s1.GetPrice("AAAAAAB"));  // A * 6 = 60    60* 0.9 = 54        54 +100 = 154
        }
        [TestMethod]
        public void ComboDiscount()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 20);
            s1.RegisterProduct('C', 50);
            s1.RegisterProduct('D', 100);
            s1.RegisterComboDiscount("ABC", 60, false);
            Assert.AreEqual(110, s1.GetPrice("CAAAABB"));
        }
        [TestMethod]
        public void CountDiscountWithMembership()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('E', 50);
            s1.RegisterCountDiscount('A', 3, 4, true);
            s1.RegisterSuperShopMember("1");
            Assert.AreEqual(190*0.9, s1.GetPrice("AAAAAEEEv1")); // 5* A(10) = 50    3 * E(50) = 150    40+ 150 = 190
        }
        [TestMethod]
        public void AmountDiscountWithMembership()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 100);
            s1.RegisterAmountDiscount('A', 0.9, 5, true);
            s1.RegisterSuperShopMember("1");
            Assert.AreEqual(Math.Floor(154*0.9), s1.GetPrice("AAAAAABv1"));  // A * 6 = 60    60* 0.9 = 54        54 +100 = 154
        }
        [TestMethod]
        public void ComboDiscountWithMembership()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 20);
            s1.RegisterProduct('C', 50);
            s1.RegisterProduct('D', 100);
            s1.RegisterSuperShopMember("1");
            s1.RegisterComboDiscount("ABC", 60, true);
            Assert.AreEqual(110*0.9, s1.GetPrice("CAAAABBv1"));
        }
        [TestMethod]
        public void BuyNGetsNPlusOne()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('E', 50);
            s1.RegisterCountDiscount('A', 4, 5, false);
            Assert.AreEqual(200, s1.GetPrice("AAAAAAEEE"));
        }
        [TestMethod]
        public void SuperShopBuy()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 20);
            s1.RegisterProduct('C', 50);
            s1.RegisterSuperShopMember("1");
            s1.GetPrice("CCv1"); //100*0.01 = 1
            Assert.AreEqual(9, s1.GetPrice("Av1p")); // 10-1 = 9
        }
        [TestMethod]
        public void MultipleDiscounts()// All discounts count from the original price
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 20);
            s1.RegisterProduct('C', 50);
            s1.RegisterAmountDiscount('A',0.9,2, false);
            s1.RegisterComboDiscount("AB", 10, false);
            s1.RegisterCountDiscount('B', 2, 3, false);
            Assert.AreEqual(106,s1.GetPrice("AAAABBBC")); //Original 10*4 + 20*3 + 50 = 150     Discounts 4 + 20 + 20
        }
        [TestMethod]
        public void MultipleNumberID()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 20);
            s1.RegisterProduct('C', 50);
            s1.RegisterSuperShopMember("11");
            s1.GetPrice("CCv11"); //100*0.01 = 1
            Assert.AreEqual(9, s1.GetPrice("Av11p")); // 10-1 = 9
        }
        [TestMethod]
        public void HugeNumberID()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 20);
            s1.RegisterProduct('C', 50);
            s1.RegisterSuperShopMember("1111111");
            s1.GetPrice("CCv1111111"); //100*0.01 = 1
            Assert.AreEqual(9, s1.GetPrice("Av1111111p")); // 10-1 = 9
        }
        [TestMethod]
        public void ShopWithCoupon()
        {
            s1.RegisterProduct('A', 10);
            s1.RegisterProduct('B', 20);
            s1.RegisterCoupon("112554", 0.9); // -10% kupon
            s1.GetPrice("AABk112554");  // 40*0.9
            Assert.AreEqual(40,s1.GetPrice("AABk112554"));  // 40, mert már elhasználták a kupont
        }
    }
}
