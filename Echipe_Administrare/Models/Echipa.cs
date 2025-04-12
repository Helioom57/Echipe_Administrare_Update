using System;
using System.Collections.Generic;
using System.Linq;

namespace Echipe_Administrare.Models

{
    public class Echipa
    {
        public string NumeEchipa { get; set; }
        public List<Jucator> Jucatori { get; set; }

        public Echipa(string numeEchipa)
        {
            if (string.IsNullOrWhiteSpace(numeEchipa))
                throw new ArgumentException("Numele echipei nu poate fi gol.");
            NumeEchipa = numeEchipa;
            Jucatori = new List<Jucator>();
        }

        public void AdaugaJucator(Jucator jucator)
        {
            Jucatori.Add(jucator);
        }

        public double SalariulTotalEchipei() => Jucatori.Sum(j => j.Salariu);

        public bool EliminaJucator(Jucator jucator) => Jucatori.Remove(jucator);

    }
}