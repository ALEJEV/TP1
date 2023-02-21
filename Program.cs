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

            //si l'output existe, on le supprime
            if (File.Exists("Status.csv"))
            {
                File.Delete("Status.csv");

            }


            //Partie comptes 
            List<Compte> Banque = new List<Compte>();

            using (StreamReader file = new StreamReader("Comptes.csv"))
            {

                string line;


                while ((line = file.ReadLine()) != null)
                {

                    line = line.Replace(".", ",");
                    

                    string[] subs = line.Split(';'); //Séparation des éléments du .CSV

                    // On remplace les points par des virgules


                    /*
                    foreach (string s in subs)
                    {
                        Console.WriteLine($"{s}");
                    }
                    Console.WriteLine("--------");*/

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

            /*
            //Affichage des comptes : 
            foreach (Compte cp in Banque) 
            {
                cp.afficher();

            }

            Console.ReadKey();*/


            //Partie transactions : 

            using (StreamReader file = new StreamReader("Transactions.csv"))
            {
                string line;


                while ((line = file.ReadLine()) != null)
                {
                    line = line.Replace(".", ","); //On remplace les . par des , au cas où
                    string[] subs = line.Split(';'); //Séparation des éléments du .CSV


                    // Gestion de l'ID des transactions (si l'ID existe déjà, on ignore)
                    if (int.TryParse(subs[0], out int d)) //si on arrive à convertir le premier élément en int...

                    {
                        if (d <= 0 || Compte.TransactionList.Contains(d)) //Si d est négatif ou égal à 0 ou
                        {                                            //Si l'ID existe déjà
                            continue;                                  //On ne fait rien

                        }

                    }

                    bool LigneIsOK = ligneEstOK(subs);

                    if (!LigneIsOK) // SI la ligne n'est pas OK
                    {

                        //line = line.Replace(",", ".");

                        //File.AppendAllText("Status.csv", line);
                        continue;
                    }



                    // A partir de là, on implique les objets

                    bool statutTransaction = false;
                    int expediteur = int.Parse(subs[2]);
                    int destinataire = int.Parse(subs[3]);
                    double argent = double.Parse(subs[1]);


                    // partie dépot argent
                    if (expediteur == 0 && Compte.CompteList.Contains(destinataire))
                    {

                        foreach (Compte cpt in Banque) 
                        {
                            if (cpt._ID == destinataire)
                            {
                                statutTransaction = cpt.deposerArgent(argent);
                                break;

                            }

                        }

                    }

                    // partie retrait argent

                    else if (destinataire == 0 && Compte.CompteList.Contains(expediteur))
                    {

                        foreach (Compte cpt in Banque)
                        {
                            if (cpt._ID == expediteur)
                            {
                                statutTransaction = cpt.retirerArgent(argent);
                                break;

                            }

                        }

                    }


                    // partie virement
                    else if (Compte.CompteList.Contains(destinataire) && Compte.CompteList.Contains(expediteur))
                    {

                        foreach (Compte cpt in Banque)//on cherche l'expediteur
                        {
                            if (cpt._ID == expediteur)
                            {
                                statutTransaction = cpt.Virement(argent);

                                if (statutTransaction)// si la transaction s'est bien passée, on maj le destinataire
                                {
                                    foreach (Compte cpt2 in Banque)
                                    {
                                        if (cpt2._ID == destinataire)
                                        {
                                            cpt2.ajout_argent(argent);


                                        }


                                    }
                                }



                                break;

                            }

                        }

                        

                    }


                    if (statutTransaction)
                    {
                        File.AppendAllText("Status.csv", subs[0] + ";" + "OK"+ "\n");

                    }

                    else
                    {
                        File.AppendAllText("Status.csv", subs[0] + ";" + "KO" + "\n");

                    }

                }



                file.Close();
            }

                //Console.ReadKey();

                foreach (Compte cc in Banque)
            {
                cc.afficher();
            }
            Console.ReadKey();

            }

        static bool ligneEstOK(string[] line)
        {


            if (line.Count() != 4) //Si on a plus de 4 éléments la ligne n'est pas OK
            {
                return false;

            }



            if (line[2] == line[3]) //Si l'éméteur est le receveur sont identiques la ligne n'est pas ok
            {
                return false;
            }
            else if (!(int.TryParse(line[2], out int two)) || !(int.TryParse(line[3], out int three)))
                {
                return false;

            }


            if (double.TryParse(line[1], out double d)) //si on peut parser le montant en double
            {
                if (d <= 0) //Si le montant parsé est inférieur ou égal à 0
                {
                    return false; // la ligne n'est pas ok
                }


            }
            else { return false; }//si on ne peut pas parser le montant en double, la ligne n'est pas ok






            return true;
        }




    }
}
