using System;


namespace Echipe_Administrare.Models
{
    public enum PozitieJucator
    {
        Portar,
        Fundas,
        Mijlocas,
        Atacant,
    }

    public class Jucator
    {
        public string Nume { get; set; }
        public DateTime DataNasterii { get; set; }
        public PozitieJucator Pozitie { get; set; }
        public double Salariu { get; set; }

        public Jucator(string nume, DateTime dataNasterii, PozitieJucator pozitie, double salariu)
        {
            if (string.IsNullOrWhiteSpace(nume))
                throw new ArgumentException("Nume invalid.");
            if (salariu <= 0)
                throw new ArgumentException("Salariu trebuie să fie pozitiv.");

            Nume = nume;
            DataNasterii = dataNasterii;
            Pozitie = pozitie;
            Salariu = salariu;
        }
    }
}