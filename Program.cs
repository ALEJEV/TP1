using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Compte> Banque = new List<Compte>();

            using (StreamReader file = new StreamReader("Comptes.csv"))
            {

                string line;


                while ((line = file.ReadLine()) != null)
                {

                    line = line.Replace(".", ",");
                    

                    string[] subs = line.Split(';'); //Séparation des éléments du .CSV

                    // On remplace les points par des virgules



                    foreach (string s in subs)
                    {
                        Console.WriteLine($"{s}");
                    }
                    Console.WriteLine("--------");

                    if (subs.Count() < 1 || subs.Count() > 2) //Si le nb d'elements est deff de 1 et 2, on ne fait rien
                    {
                        continue;   
                    }

                    if (int.TryParse(subs[0], out int d)) //si on arrive à convertir le premier élément en int...

                    {
                        if (d <= 0 || Compte.CompteList.Contains(d)) //Si d est négatif ou égal à 0 ou
                        {                                            //Si l'ID existe déjà
                            continue;                                  //On ne fait rien

                        }

                    }

                    // Si le count == 1 alors on crée un nouveau compte

                    if (subs.Count() == 1 && d != 0)
                    {
                        Banque.Add(new Compte(d));
                        continue;

                    }

                    //SI le count == 2 alors on vérifier que la deuxième entrée est parsable en double avant
                    //de créer un nouveau compte


                    if (subs.Count() == 2)
                    {
                        if (double.TryParse(subs[1], out double e))
                        {
                            Banque.Add(new Compte(d, e));
                            // Console.WriteLine("on passe par count == 2");
                            

                        }
                        else if (subs[1] == "" && subs[0] != "")
                        { 
                        Banque.Add(new Compte(d)); //Dans le cas où on a un nombre suivi d'un point virgule seul
                        }



                        continue;

                    }






                }



                file.Close();
            }


            //Affichage des comptes : 
            foreach (Compte cp in Banque) 
            {
                cp.afficher();

            }

            Console.ReadKey();
        }

        


    }
}
