using LibraryManagement.Models;
using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        

        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            AccountManagement.Logoff();
            string url = this.Request.UrlReferrer.AbsolutePath;
            return Redirect(url);

        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        // Gestion de la connexion des Students. 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(StudentsManagersViewModels model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // C'est ici que l'on va faire la connexion complète avec la BD

            // Création de la connexion avec la BD
            libraryManagementEntities db = new libraryManagementEntities();
            string password;



            // À l'aide des Regular Expression, on regarde si le numéro de téléphone est du bon format. 
            string strIn = model.student.Email;
            string re1 = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$";  // RegEx du numero de telephone

            Regex r = new Regex(re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(strIn);
            if (m.Success)
            {
                // on commence par regardé si l'adresse est trouvé en BD
                if (db.Student.Any(o => o.PhoneNumber == model.student.Email))
                {
                    // On select le student dans la BD
                    Student student = db.Student.Single(o => o.PhoneNumber == model.student.Email);

                    // puis on compare les 2 password
                    password = UtilResources.EncryptPassword(model.student.StudentPassword);

                    if (password == student.StudentPassword)
                    {
                        // On ajoute le nom de l'utilisateur dans la variable global
                        // TODO AJOUTER LE STUDENT NEW STUDENT        

                        // AFFICHER QUI EST CONNECTER DANS LA BAR EN HAUT
                        AccountManagement.nomUtilisateur = student.FirstName;
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", UtilResources.GetLabel("Le numéro de téléphone et le mot de passe ne correspondent pas"));
                        return View(model);
                    }
                }
            }

            // on commence par regardé si l'adresse est trouvé en BD
            if (db.Student.Any(o => o.Email == model.student.Email))
            {

                // On select le student dans la BD
                Student student = db.Student.Single(o => o.Email == model.student.Email);

                // puis on compare les 2 password
                password = UtilResources.EncryptPassword(model.student.StudentPassword);

                if (password == student.StudentPassword)
                {
                    // On ajoute le nom de l'utilisateur dans la variable global
                    AccountManagement.nomUtilisateur = student.FirstName;
                    return RedirectToLocal(returnUrl);

                }
                else
                {
                    ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel et le mot de passe ne correspondent pas"));

                    return View(model);
                }
            }
            else // On on ne trouve pas le email
            {
                ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel et le mot de passe ne correspondent pas"));

                return View(model);
            }
        }


        // Gestion de la connexion des Managers
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginManagers(StudentsManagersViewModels model, string returnUrl)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // C'est ici que l'on va faire la connexion complète avec la 
            // Création de la connexion avec la BD
            libraryManagementEntities db = new libraryManagementEntities();
            string password;

            // À l'aide des Regular Expression, on regarde si le numéro de téléphone est du bon format. 
            string strIn = model.manager.Email;


            // on commence par regardé si l'adresse est trouvé en BD
            if (db.Manager.Any(o => o.Email == model.manager.Email))
            {
                // On select le student dans la BD
                Manager manager = db.Manager.Single(o => o.Email == model.manager.Email);

                // puis on compare les 2 password
                password = UtilResources.EncryptPassword(model.manager.ManagerPassword);

                if (password == manager.ManagerPassword)
                {
                    // On ajoute le nom de l'utilisateur dans la variable global
                    AccountManagement.nomUtilisateur = manager.FirstName;
                    return Redirect(url);
                }
                else
                {
                    ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel et le mot de passe ne correspondent pas"));
                    return Redirect(url);
                }
            }
            else // On on ne trouve pas le email
            {
                ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel et le mot de passe ne correspondent pas"));
                return Redirect(url);
            }
        }

        // Méthode qui retourne l'utilisateur à l'index. 
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}