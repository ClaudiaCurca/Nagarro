using iQuest.Terra;
using System.Collections.Generic;

namespace iQuest.TerraPlus
{
    internal class CountryComparatorByCapital : IComparer<Country>
    {
        public int Compare(Country x, Country y)
        {
            if (x == y) 
            {
                return 0;
            }
            if (x == null) 
            {
                return -1; 
            }
            if(y == null) 
            {
                return 1; 
            }
            return x.Capital.CompareTo(y.Capital);
        }
    }
}
