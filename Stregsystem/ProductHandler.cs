using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stregsystem
{
    class ProductHandler
    {
        public List<Product> productList;

        public ProductHandler()
        {
            productList = new List<Product>();

            GetProductList();
            HandleSeasonalProducts();
        }

        private void HandleSeasonalProducts()
        {
            SeasonalProduct temp;

            foreach (Product product in productList)
            {
                temp = product as SeasonalProduct;

                if(temp != null)
                    if ((temp.SeasonStartDate <= DateTime.Today || temp.SeasonStartDate == null) && (temp.SeasonEndDate > DateTime.Today || temp.SeasonEndDate == null))
                        temp.Active = true;
                    else
                        temp.Active = false;
            }
        }

        private void GetProductList()
        {
            List<string> strings = ReadStringsFromFile();

            foreach (string str in strings)
            {
                string[] productString = RemoveHTMLTags(str).Split(';');

                try
                {
                    productList.Add(new Product(Convert.ToInt32(productString[0]), productString[1], Convert.ToInt32(productString[2]), false, (Convert.ToInt32(productString[3]) == 1)));
                }
                catch
                {

                }
            }
        }

        private List<string> ReadStringsFromFile()
        {
            StreamReader reader = new StreamReader(@".\products.csv");
            List<string> results = new List<string>();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                results.Add(line);
            }

            return results;
        }

        private string RemoveHTMLTags(string str)
        {
            return Regex.Replace(str, "<.*?>", string.Empty);
        }
    }
}