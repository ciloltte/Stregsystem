using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class Product
    {
        public int ProductID { get { return productID; } }

        public string Name 
            { get { return name; } set { name = value; } }

        public int Price 
            { get { return price; } set { price = value; } }

        public bool Active 
            { get { return active; } set { active = value; } }

        public bool CanBeBoughtOnCredit 
            { get { return CanBeBoughtOnCredit; } set { CanBeBoughtOnCredit = value; } }

        private int productID;
        private string name;
        private int price;
        private bool active;
        private bool canBeBoughtOnCredit;

        public Product(int productID, string name, int price, bool canBeBoughtOnCredit, bool active)
        {
            this.productID = productID;
            this.name = name;
            this.price = price;
            this.canBeBoughtOnCredit = canBeBoughtOnCredit;
            this.active = active;
        }

        public override string ToString()
        {
            return ProductID + "\t" + Name + "\t" + (float)(Price / 100);
        }
    }
}
