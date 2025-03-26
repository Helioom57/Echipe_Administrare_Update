using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Echipe_Administrare

{
    public class Sortare
    {
        public static void CitesteSiAfiseazaCuvinte()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "cuvinte.txt");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Fisierul nu a fost gasit!");
                return;
            }


            var categorii = File.ReadAllLines(filePath)
                               .Where(word => !string.IsNullOrEmpty(word))
                               .GroupBy(word => char.ToLower(word[0]))
                               .ToDictionary(g => g.Key, g => g.ToList());


            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                if (categorii.ContainsKey(letter))
                {
                    Console.WriteLine($"Cuvinte care incep cu '{char.ToUpper(letter)}':");
                    foreach (var word in categorii[letter])
                    {
                        Console.WriteLine(word);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}