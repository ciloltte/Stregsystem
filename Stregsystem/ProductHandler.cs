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
        public List<Product> GetProductList()
        {
            List<string> strings = ReadStringsFromFile();
            List<Product> result = new List<Product>();

            foreach (string str in strings)
            {
                string[] productString = RemoveHTMLTags(str).Split(';');

                try
                {
                    result.Add(new Product(Convert.ToInt32(productString[0]), productString[1], Convert.ToInt32(productString[2]), false, (Convert.ToInt32(productString[3]) == 1)));
                }
                catch
                {

                }
            }

            return result;
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
