using Echipe_Administrare;
using System.Collections.Generic;
using System;
using System.IO;


namespace Echipe_Administrare
{
    class AdministrareEchipe_Memorie
    {
        private List<Echipa> echipe;
        private const string FilePath = "teams.txt";

        public void SaveData()
        {
            var lines = new List<string>();
            foreach (var team in echipe)
            {
                lines.Add($"TEAM:{team.NumeEchipa}");
                foreach (var player in team.Jucatori)
                {
                    lines.Add($"{player.Nume}|{player.DataNasterii:dd/MM/yyyy}|{player.Pozitie}|{player.Salariu}");
                }
            }
            File.WriteAllLines(FilePath, lines);
        }

        private void LoadTeams()
        {
            if (!File.Exists(FilePath)) return;

            var lines = File.ReadAllLines(FilePath);
            Echipa currentTeam = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("TEAM:"))
                {
                    currentTeam = new Echipa(line.Substring(5));
                    echipe.Add(currentTeam);
                }
                else if (currentTeam != null && line.Contains("|"))
                {
                    var parts = line.Split('|');
                    currentTeam.AdaugaJucator(new Jucator(
                        parts[0],
                        DateTime.Parse(parts[1]),
                        (PozitieJucator)Enum.Parse(typeof(PozitieJucator), parts[2]),
                        double.Parse(parts[3])
                    ));
                }
            }
        }

        public AdministrareEchipe_Memorie()
        {
            echipe = new List<Echipa>();
            LoadTeams();
        }

        public void AdaugaEchipa(Echipa echipa)
        {
            echipe.Add(echipa);
        }

        public Echipa GasesteEchipa(string nume)
        {
            return echipe.Find(e => e.NumeEchipa == nume);
        }

        public void AfiseazaEchipe()
        {
            foreach (var echipa in echipe)
            {
                List<string> info = echipa.ObtineInformatiiEchipa();
                foreach (var linie in info)
                {
                    Console.WriteLine(linie);
                }
            }
        }
        
    }
}

