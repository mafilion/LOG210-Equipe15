using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Utils
{
    public static class AccountManagement
    {
        // Quand il n'y a pas d'utilisateur connecter, on met rien dans les nom
        public static string nomUtilisateur = "";
        public static bool estManager = false;
        public static int IDAccount = -1;

        public static void Login(string username, bool isManager)
        {
             nomUtilisateur = username;
             estManager = isManager;
        }

        // Déconecte l'utilisateur
        public static void Logoff()
        {
            nomUtilisateur = "";
            estManager = false;
            IDAccount = -1;
        }


        // retourne s'il y a quelqu'un de connecté
        public static string isConnected()
        {
            // Si quelqu'un est connecté, on regarde s'il est student ou manager
            if (nomUtilisateur != "")
            {
                if (estManager == true)
                    return "manager";
                else
                    return "student";
            }
            else
                return null; // S'il y a personne de connecter
        }

        


    }
}