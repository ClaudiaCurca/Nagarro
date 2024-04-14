using System;
using System.Collections.Generic;
using System.Linq;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class SalesRepository : ISalesRepository
    {
        private List<Sales> saleList = new List<Sales>();

        public Sales Add(Sales sale)
        {
            saleList.Add(sale);
            return sale;
        }

        public List<Sales> GetAll()
        {
            List<Sales> list = new List<Sales>();

            foreach (Sales sale in saleList)
            {
                list.Add(sale);
            }
            return list;
        }

        public List<Sales> GetByDate(DateTime startDate, DateTime endDate)
        {
            return saleList.Where(x=>x.Date<endDate&&x.Date>startDate).ToList();
        }
    }
}
