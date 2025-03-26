using System;

namespace Echipe_Administrare
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
            Nume = nume;
            DataNasterii = dataNasterii;
            Pozitie = pozitie;
            Salariu = salariu;
        }

        public override string ToString()
        {
            return $"Nume: {Nume}, Data nasterii: {DataNasterii:dd.MM.yyyy}, Pozitie: {Pozitie}, Salariu: {Salariu} RON";
        }
        
    }
}