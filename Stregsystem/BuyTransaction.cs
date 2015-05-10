using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class BuyTransaction : Transaction
    {
        public Product Product { get { return product; } }
        public int AmountOfProduct { get { return amountOfProduct; } }

        private Product product;
        private int amountOfProduct;

        public BuyTransaction(int transactionID, User user, Product product)
            : base(transactionID, user, -product.Price)
        {
            this.product = product;
            this.amountOfProduct = 1;
        }

        public BuyTransaction(int transactionID, User user, Product product, int amountOfProduct)
            : base(transactionID, user, -product.Price * amountOfProduct)
        {
            this.product = product;
            this.amountOfProduct = amountOfProduct;
        }

        public override void Execute()
        {
            if (User.Balance >= -Amount || product.CanBeBoughtOnCredit)
                base.Execute();
            else
                throw new InsufficientCreditsException(User, Product);
        }

        public override string ToString()
        {
            return "BuyTransaction:" + base.ToString() + " Product=" + (amountOfProduct > 0 ? amountOfProduct + "x" + product.Name : product.Name);
        }
    }
}
