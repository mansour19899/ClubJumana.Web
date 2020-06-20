using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
     public  class CountryComparer: IEqualityComparer<Country>
    {
        public bool Equals(Country x, Country y)
        {

            //First check if both object reference are equal then return true
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }



            //If either one of the object refernce is null, return false
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            //Comparing all the properties one by one
            return x.Id == y.Id && x.Name == y.Name && x.Code == y.Code && x.Duty == y.Duty && x.ExChangeRate == y.ExChangeRate && x.Currency == y.Currency && x.CurrencyName == y.CurrencyName && x.DigitalCode == y.DigitalCode;

        }

        public int GetHashCode(Country obj)
        {
            //If obj is null then return 0
            if (obj == null)
            {
                return 0;
            }
            //Get the ID hash code value
            int IDHashCode = obj.Id.GetHashCode();
            //Get the Name HashCode Value
            int NameHashCode = obj.Name == null ? 0 : obj.Name.GetHashCode();
            return IDHashCode ^ NameHashCode;


        }

     
    }
}
