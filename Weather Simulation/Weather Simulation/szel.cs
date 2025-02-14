using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Simulation
{
    internal class Szel
    {
        public string szelirany { get; private set; }

        public double szelsebesseg { get; private set; }
        private List<string> Szeliranyok { get; set; } = new List<string>()
        {
            "Nyugati",
            "Északnyugati",
            "Északi",
            "Északkeleti",
            "Keleti",
            "Délkeleti",
            "Déli",
            "Délnyugati"
        };
            /// szögekben > int , max 360

        /// Szélirány megint random
        

        public Szel()
        {
            szelirany = randomSzelirany();
            szelsebesseg = randomSebesseg();
        }

        private string randomSzelirany()
        {
            Random random = new Random();
            int szam = random.Next(0, Szeliranyok.Count);
            for (int i = 0; i < Szeliranyok.Count; i++)
            {
                if (szam == i)
                {
                    szelirany = Szeliranyok[i];
                }

            }
            return szelirany;
             
        }

        private double randomSebesseg()
        {
            Random random = new Random();
            int szelsebesseg = random.Next(50,280);
            return szelsebesseg / 10.0;
        }
     
    }
}
