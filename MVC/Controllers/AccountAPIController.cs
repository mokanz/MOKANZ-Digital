using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOKANZ.Models;
using System.Threading.Tasks;

namespace MOKANZ.Controllers
{
    
    public class AccountAPIController : ApiController
    {
        MOKANZEntities1 db = new MOKANZEntities1();

        //contact type to populate dropdown on the register page
        [HttpGet]
        public IEnumerable<ContactTypeModel> ContactTypes()
        {
            var list = db.ContactTypes.ToList()
                .Select(a=> new ContactTypeModel { ContactTypeID = a.ContactTypeID, ContactType = a.ContactType1 });

            return list;
        }


        //Address types to populate dropdown on the register page
        [HttpGet]
        public IEnumerable<AddressTypeModel> AddressTypes()
        {
            var list = db.AddressTypes.ToList()
                .Select(a => new AddressTypeModel { AddressTypeID = a.TypeID, AddressType = a.Type });

            return list;
        }

        [HttpPost]
        public async Task<string> Register(RegisterViewModel model) {

            try
            {
                Customer customer = new Customer();
                customer.UserName = model.Customer.UserName;
                customer.Password = model.Customer.Password;
                customer.FirstName = model.Customer.FirstName;
                customer.LastName = model.Customer.LastName;

                db.Customers.Add(customer);
                
                customer.Addresses.Add(model.Address);
                customer.Contacts.Add(model.Contact);
                await db.SaveChangesAsync();
                return "Registration successful. Please proceed to login page to log in.";
            }
            catch (Exception e) {
                return e.Message;
            }
           
        }
    }
}
