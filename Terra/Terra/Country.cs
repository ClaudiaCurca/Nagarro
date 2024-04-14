using System;
using System.Diagnostics.CodeAnalysis;

namespace iQuest.Terra
{
    public class Country : IComparable<object>, IEquatable<Country>
    {
        public string Name { get; }

        public string Capital { get; }

        public Country(string name, string capital)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Capital = capital ?? throw new ArgumentNullException(nameof(capital));
        }
        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }
        //    if (!(obj is Country))
        //    {
        //        return false;
        //    }
        //    Country countryToCompare = (Country)obj;
        //    return (this.Name == countryToCompare.Name)
        //        && (this.Capital == countryToCompare.Capital);

        //}
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Capital);
        }

        public int CompareTo(object o)
        {
            if (o == null)
            {
                return 1;
            };
            if (!(o is Country))
            {
                throw new ArgumentException();
            };

            Country country = (Country)o;
            int result = Name.CompareTo(country.Name);
            if (result == 0)
            {
                result = Capital.CompareTo(country.Capital);
            }
            return result;
        }

        public bool Equals([AllowNull] Country country)
        {
            if (country == null)
            {
                return false;
            };
            if (!(country is Country))
            {
                return false;
            };

            return (this.Name == country.Name)
                && (this.Capital == country.Capital);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return Equals(obj as Country);
        }
    }
}