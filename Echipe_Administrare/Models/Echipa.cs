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

        public bool EliminaJucator(Jucator jucator)
        {
            var jucatorToRemove = Jucatori.FirstOrDefault(j => j.Nume == jucator.Nume);
            if (jucatorToRemove != null)
            {
                return Jucatori.Remove(jucatorToRemove);
            }
            return false;
        }

        public double SalariulTotalEchipei() => Jucatori.Sum(j => j.Salariu);



    }
}