using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace YelpUI
{
    public class Business
    {
        public string BusinessID { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public double Stars { get; set; }
        public int NumTips { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public int NumCheckIns { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public void ExecuteQuery()
        {

        }
    }
}
