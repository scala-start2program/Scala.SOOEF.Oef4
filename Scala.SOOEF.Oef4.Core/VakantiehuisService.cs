using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scala.SOOEF.Oef4.Core
{
    public class VakantiehuisService
    {
        public List<Vakantiehuis> Vakantiehuizen { get; private set; }
        public VakantiehuisService()
        {
            Vakantiehuizen = new List<Vakantiehuis>();
            DoSeeding();
            OrderList();
        }
        private void DoSeeding()
        {
            Vakantiehuizen.Add(new Vakantiehuis("Zonnebloem", "Klaverstraat 1", "8000 Brugge", 75M, 2));
            Vakantiehuizen.Add(new Vakantiehuis("Papaver", "Stoofstraat 5", "8000 Brugge", 95M, 2));
            Vakantiehuizen.Add(new Vakantiehuis("Pissebloem", "Steenstraat 100", "8000 Brugge", 185M, 6));
        }

        public void OrderList()
        {
            Vakantiehuizen = Vakantiehuizen.OrderBy(o => o.Huisnaam).ToList();
        }
        public void VoegVakantiehuisToe(Vakantiehuis vakantiehuis)
        {
            Vakantiehuizen.Add(vakantiehuis);
            OrderList();
        }
        public void VerwijderVakantiehuis(Vakantiehuis vakantiehuis)
        {
            Vakantiehuizen.Remove(vakantiehuis);
        }

    }
}
