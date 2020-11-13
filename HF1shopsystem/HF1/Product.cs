using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF1
{
    class Product
    {
        public char name { get; set; }
        public int price { get; set; }
        public Product(char name, int price)
        {
            this.name = name;
            this.price = price;
        }
    }
}
