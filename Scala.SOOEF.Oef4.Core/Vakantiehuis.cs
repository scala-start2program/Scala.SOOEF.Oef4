using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scala.SOOEF.Oef4.Core
{
    public class Vakantiehuis
    {
        private string id;
        private string adres;
        private string huisnaam;
        private string gemeente;
        private decimal prijsPerNacht;
        private byte maxPersonen;


        public string ID
        {
            get { return id; }
        }

        public string Huisnaam
        {
            get { return huisnaam; }
            set
            {
                value = value.Trim();
                if (value == "")
                {
                    throw new Exception("Huisnaam kan niet leeg zijn");
                }
                else
                {
                    huisnaam = value;
                }
            }
        }
        public string Adres  // straat + huisnummer
        {
            get { return adres; }
            set { adres = value; }
        }
        public string Gemeente // postcode + gemeente
        {
            get { return gemeente; }
            set { gemeente = value; }
        }
        public decimal PrijsPerNacht
        {
            get { return prijsPerNacht; }
            set
            {
                if (value < 0)
                    value = 0;
                prijsPerNacht = value;
            }
        }
        public byte MaxPersonen
        {
            get { return maxPersonen; }
            set
            {
                if (value > 12)
                    value = 12;
                maxPersonen = value;
            }
        }

        public Vakantiehuis()
        {
            id = Guid.NewGuid().ToString();
        }
        public Vakantiehuis(string huisnaam, string adres, string gemeente, decimal prijsPerNacht, byte maxPersonen)
        {
            id = Guid.NewGuid().ToString();
            Huisnaam = huisnaam;
            Adres = adres;
            Gemeente = gemeente;
            PrijsPerNacht = prijsPerNacht;
            MaxPersonen = maxPersonen;
        }
        public override string ToString()
        {
            return $"{huisnaam} - {gemeente}";
        }



    }
}
