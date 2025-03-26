using System;
using System.Collections.Generic;
using System.Linq;

namespace Echipe_Administrare

{
    public class Echipa
    {
        public string NumeEchipa { get; set; }
        public List<Jucator> Jucatori { get; set; }

        public Echipa(string numeEchipa)
        {
            NumeEchipa = numeEchipa;
            Jucatori = new List<Jucator>();
        }

        public void AdaugaJucator(Jucator jucator)
        {
            Jucatori.Add(jucator);
        }

        public List<string> ObtineInformatiiEchipa()
        {
            List<string> info = new List<string> { $"Echipa {NumeEchipa}:" };
            foreach (var jucator in Jucatori)
            {
                info.Add(jucator.ToString());
            }
            return info;
        }

        public double SalariulTotalEchipei()
        {
            double salariuTotal = 0;
            foreach (var jucator in Jucatori)
            {
                salariuTotal += jucator.Salariu;
            }
            return salariuTotal;
        }
        
    }
}