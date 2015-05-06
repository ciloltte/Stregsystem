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
            { get { return Price; } set { Price = value; } }

        public bool Active 
            { get { return Active; } set { Active = value; } }

        public bool CanBeBoughtOnCredit 
            { get { return CanBeBoughtOnCredit; } set { CanBeBoughtOnCredit = value; } }

        private int productID;
        private string name;
        private int price;
        private bool active;
        private bool canBeBoughtOnCredit;

        public Product(int productID, string name, int price, bool canBeBoughtOnCredit, bool active)
        {
            this.name = name;
            this.price = price;
            this.canBeBoughtOnCredit = canBeBoughtOnCredit;
            this.active = active;
        }
    }
}
