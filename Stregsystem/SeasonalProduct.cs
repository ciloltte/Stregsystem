using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }

        private DateTime seasonStartDate;
        private DateTime seasonEndDate;

        public SeasonalProduct(int productID, string name, int price, bool canBeBoughtOnCredit, bool active, DateTime seasonStartDate, DateTime seasonEndDate)
            : base(productID, name, price, canBeBoughtOnCredit, active)
        {
            this.seasonStartDate = seasonStartDate;
            this.seasonEndDate = seasonEndDate;
        }
    }
}
