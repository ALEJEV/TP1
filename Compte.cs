using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Console.WriteLine($"Affichage du compte {_ID}");
            Console.WriteLine($"Solde actuel = {_solde} cacahuète(s)\n");
 
        }

        protected bool transactionOK()

        { return _solde != 0; 
        }

        public bool deposerArgent(double argentADeposer)
        {
            Console.WriteLine("On est dans deposer argent");

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

            else
            {
                double totalArgent = 0;
                foreach (double cht in _histoTransac)
                {
                    totalArgent += cht;
                }
                totalArgent += argentADeposer;
                if (totalArgent > 1000)
                { return false; }

                Console.WriteLine($"total argent = {totalArgent}");


            }



            _solde += argentADeposer;
            _histoTransac.Add(argentADeposer);
            return true;
        }
        public bool retirerArgent(double argentARetirer)
        {
            Console.WriteLine("On est dans retirer argent");


            int countHistoTransac = _histoTransac.Count;


            if (countHistoTransac > 9) //s'il y a plus de 10 entrées dans historique transaction
            {
                double totalArgent = 0;
                for (int i = (countHistoTransac - 9); i < countHistoTransac; i++) // On regarde la somme des 10 dern retraits
                {
                    totalArgent += _histoTransac[i];
                }

                totalArgent+=argentARetirer;
                if (totalArgent > 1000)
                { return false; }

            }

            else
            {
                double totalArgent = 0;
                foreach (double cht in _histoTransac)
                {
                    totalArgent += cht;
                }
                totalArgent += argentARetirer;
                if (totalArgent > 1000)
                { return false; }

                Console.WriteLine($"total argent = {totalArgent}");


            }

            if (_solde < argentARetirer)
            {
                return false;
            }

            _solde -= argentARetirer;
            _histoTransac.Add(argentARetirer);
            return true;
        }




        public bool Virement(double argentAVirer)
        {
            Console.WriteLine("On est dans virement");
            Console.WriteLine($"Argent à virer = {argentAVirer}");



            int countHistoTransac = _histoTransac.Count;


            if (countHistoTransac > 9) //s'il y a plus de 10 entrées dans historique transaction
            {
                double totalArgent = 0;
                for (int i = (countHistoTransac - 9); i < countHistoTransac; i++) // On regarde la somme des 10 dern retraits
                {
                    totalArgent += _histoTransac[i];
                }

                totalArgent += argentAVirer;
                if (totalArgent > 1000)
                { return false; }

                Console.WriteLine($"total argent = {totalArgent}");

            }

            else
            {
                double totalArgent = 0;
                foreach (double cht in _histoTransac)
                {
                    totalArgent += cht;
                }
                totalArgent += argentAVirer;
                if (totalArgent > 1000)
                { return false; }

                Console.WriteLine($"total argent = {totalArgent}");


            }

            if (_solde < argentAVirer)
            {
                return false;
            }

            _solde -= argentAVirer;
            _histoTransac.Add(argentAVirer);
            return true;
        }

        public void ajout_argent(double argent)
        {
            Console.WriteLine("On est dans ajouter argent apres virement");

            _solde += argent;
        
        }


    }
}
