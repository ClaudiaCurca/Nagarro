using System;
using System.Collections.Generic;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal interface ISalesRepository
    {
        Sales Add(Sales sale);

        List<Sales> GetAll();

        public List<Sales> GetByDate(DateTime startDate, DateTime endDate);
    }
}