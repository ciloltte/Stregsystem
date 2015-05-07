using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Stregsystem stregsystem = new Stregsystem();
            StregsystemCLI cli = new StregsystemCLI(stregsystem);

            cli.Start();
        }
    }
}
