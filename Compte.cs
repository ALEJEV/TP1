using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Compte
    {
        public static List<int> CompteList = new List<int>();
        int _ID;
        double _solde = 0;
        List<double> _histoTransac = new List<double>();

        public Compte(int ID, double solde = 0) 
        { 
            _ID = ID;
            _solde = solde;
            CompteList.Add(_ID);
        
        
        }

        public void afficher()
        {
            Console.WriteLine($"\n\n\nAffichage du compte {_ID}");
            Console.WriteLine($"Solde actuel = {_solde} cacahuète(s)\n");
            Console.WriteLine("Historique des transactions");

            if (!_histoTransac.Any())
            {
                Console.WriteLine("Aucune transaction dans l'historique");

            }
            else
            {
                foreach(double montant in _histoTransac)
                {
                    Console.WriteLine(montant);

                }
            }



        }



    }
}
