using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Compte
    {
        // variables statiques
        public static List<int> CompteList = new List<int>();
        public static List<int> TransactionList = new List<int>();
        public static List<double> _histoTransac = new List<double>();




        static double ArgentBanque = 100_000_000_000;


        // variables non statiques
        public int _ID;
        double _solde = 0;



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

        protected bool transactionOK()

        { return _solde != 0; 
        }

        public bool deposerArgent(double argentADeposer)
        {
            int countHistoTransac = _histoTransac.Count;

            if (countHistoTransac > 9) //s'il y a plus de 10 entrées dans historique transaction
            {
                double totalArgent = 0;
                for (int i = (countHistoTransac-9);i<countHistoTransac;i++) // On regarde la somme des 10 dern retraits
                {
                    totalArgent += _histoTransac[i];
                }

                totalArgent += argentADeposer;
                if (totalArgent > 1000)
                { return false; }

            }

            _solde += argentADeposer;
            _histoTransac.Add(argentADeposer);
            return true;
        }
        public bool retirerArgent(double argentARetirer)
        {
            int countHistoTransac = _histoTransac.Count;


            if (countHistoTransac > 9) //s'il y a plus de 10 entrées dans historique transaction
            {
                double totalArgent = 0;
                for (int i = (countHistoTransac - 9); i < countHistoTransac; i++) // On regarde la somme des 10 dern retraits
                {
                    totalArgent += _histoTransac[i];
                }

                totalArgent-=argentARetirer;
                if (totalArgent > 1000)
                { return false; }

            }

            if (_solde > argentARetirer)
            {
                return false;
            }

            _solde -= argentARetirer;
            _histoTransac.Add(argentARetirer);
            return true;
        }




        public bool Virement(double argentAVirer)
        {
            int countHistoTransac = _histoTransac.Count;


            if (countHistoTransac > 9) //s'il y a plus de 10 entrées dans historique transaction
            {
                double totalArgent = 0;
                for (int i = (countHistoTransac - 9); i < countHistoTransac; i++) // On regarde la somme des 10 dern retraits
                {
                    totalArgent += _histoTransac[i];
                }

                totalArgent -= argentAVirer;
                if (totalArgent > 1000)
                { return false; }

            }

            if (_solde > argentAVirer)
            {
                return false;
            }

            _solde -= argentAVirer;
            _histoTransac.Add(argentAVirer);
            return true;
        }

        public void ajout_argent(double argent)
        {
            _solde += argent;
        
        }


    }
}
