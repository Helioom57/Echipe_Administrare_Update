using System;

namespace Echipe_Administrare
{
    class Program
    {
        static void Main(string[] args)
        {
            AdministrareEchipe_Memorie administrareEchipe = new AdministrareEchipe_Memorie();

            bool running = true;
            while (running)
            {
                Console.WriteLine("Alege o optiune:");
                Console.WriteLine("1. Adauga echipa");
                Console.WriteLine("2. Adauga jucator la o echipa");
                Console.WriteLine("3. Afiseaza echipe si jucatori");
                Console.WriteLine("4. Afiseaza salariul total al unei echipe");
                Console.WriteLine("5. Citeste cuvinte din fisier si afiseaza categorii");
                Console.WriteLine("6. Salvare fisier");
                Console.WriteLine("7. Iesire");
                Console.Write("Optiunea ta: ");

                string optiune = Console.ReadLine();
                switch (optiune)
                {
                    case "1":
                        Console.Write("Nume echipa: ");
                        string numeEchipa = Console.ReadLine();
                        administrareEchipe.AdaugaEchipa(new Echipa(numeEchipa));
                        Console.WriteLine("Echipa adaugata cu succes!");
                        break;
                    case "2":
                        Console.Write("Nume echipa: ");
                        string echipaCautata = Console.ReadLine();
                        Echipa echipa = administrareEchipe.GasesteEchipa(echipaCautata);
                        if (echipa != null)
                        {
                            Console.Write("Nume jucator: ");
                            string nume = Console.ReadLine();
                            Console.Write("Data nasterii (dd.MM.yyyy): ");
                            DateTime dataNasterii = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);
                            Console.WriteLine("Pozitii disponibile:");
                            foreach (var poz in Enum.GetValues(typeof(PozitieJucator)))
                            {
                                Console.WriteLine($"- {poz}");
                            }

                            PozitieJucator pozitie;
                            do
                            {
                                Console.Write("Pozitie: ");
                            } while (!Enum.TryParse(Console.ReadLine(), true, out pozitie));

                            Console.Write("Salariu: ");
                            double salariu = double.Parse(Console.ReadLine());
                            echipa.AdaugaJucator(new Jucator(nume, dataNasterii, pozitie, salariu));
                            Console.WriteLine("Jucator adaugat cu succes!");
                        }
                        else
                        {
                            Console.WriteLine("Echipa nu a fost gasita!");
                        }
                        break;
                    case "3":
                        administrareEchipe.AfiseazaEchipe();
                        break;
                    case "4":
                        Console.Write("Nume echipa: ");
                        string echipaSalariu = Console.ReadLine();
                        Echipa echipaSalariuCautata = administrareEchipe.GasesteEchipa(echipaSalariu);
                        if (echipaSalariuCautata != null)
                        {
                            Console.WriteLine($"Salariul total al echipei {echipaSalariu}: {echipaSalariuCautata.SalariulTotalEchipei()} RON");
                        }
                        else
                        {
                            Console.WriteLine("Echipa nu a fost gasita!");
                        }
                        break;
                    case "5":
                        Sortare.CitesteSiAfiseazaCuvinte();
                        break;
                    case "6":
                        administrareEchipe.SaveData();  
                        running = false;
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
        }
    }
}