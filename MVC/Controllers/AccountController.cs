using MOKANZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Threading;
using System.Web.Security;
using MOKANZ.CustomerService;
using System.ServiceModel;
using System.Net.Http;
using System.Configuration;
using MOKANZ.Repositories;

namespace MOKANZ.Controllers
{
    public class AccountController : Controller
    {
        
        AccountsRepository repository = new AccountsRepository();
        
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login (LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                ViewBag.error = "";
                int result = 5;// await repository.Login(model.UserName, model.Password); //true;// await client.LoginAsync(model.UserName, model.Password);
                if (result > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    Session.Add("CustomerID", result);//wcf response retuens customerid
                    return RedirectToAction("Index", "Home");
                }

                else if (result == -1) //invalid username or pw
                {
                    ViewBag.error = "Invalid username or password";
                    return View(model);
                }

            }

            catch (FaultException<OperationFault> e) {

                ViewBag.error = e.Detail.Msg;

                return View(model);
            }
            
            return RedirectToAction("Index", "Home");
        }


        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        
        
        //GET: /Account/Register
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var ctypesresult = await repository.GetContactTypes(); 
            var atypesresult = await repository.GetAddressTypes();
            var view = new RegisterViewModel { ContactType = ctypesresult, AddressType = atypesresult };

            return View(view);
        }

        //POST: /Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            model.AddressType = await repository.GetAddressTypes();
            model.ContactType = await repository.GetContactTypes();


            //create new customer and add all details to database
            if (ModelState.IsValid) {

                ViewBag.regresult = await repository.Register(model);

                //if register successful, log user in and redirect to homepage?
                

            }
            else {
                @ViewBag.regresult = "Registration unsuccessful. Ensure all your info is provided.";
                    }
            return View(model);
        }
    }
}