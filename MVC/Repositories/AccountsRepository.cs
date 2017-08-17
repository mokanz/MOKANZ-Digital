using MOKANZ.CustomerService;
using MOKANZ.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MOKANZ.Repositories
{
    public class AccountsRepository
    {
        //WebAPI
        private HttpClient apiclient = new HttpClient
        {
            BaseAddress = new Uri(
                APIUri.BaseAddress
                
                )
        };

        //WCF service
        CustomerServiceClient client = new CustomerServiceClient();



        //Address Type drop down
        public async Task<IEnumerable<AddressTypeModel>> GetAddressTypes()
        {
            HttpResponseMessage atypesreply = await apiclient.GetAsync(APIUri.AccountAPI + "addresstypes"); //"api/accountapi/addresstypes"
            return atypesreply.Content.ReadAsAsync<IEnumerable<AddressTypeModel>>().Result;
            
        }

        //contact types drop down
        public async Task<IEnumerable<ContactTypeModel>> GetContactTypes()
        {
            HttpResponseMessage ctypesreply = await apiclient.GetAsync( APIUri.AccountAPI + "contacttypes"); //"api/accountapi/
            return ctypesreply.Content.ReadAsAsync<IEnumerable<ContactTypeModel>>().Result;
        }
        
        public async Task<string> Register(RegisterViewModel model) {

            var registerreply = await apiclient.PostAsJsonAsync(APIUri.AccountAPI + "register", model); //"api/accountapi/
            return await registerreply.Content.ReadAsStringAsync();
        }


        //connect to wcf service to check credentials.
        public async Task<int> Login(string username, string password) { 
        return await client.LoginAsync(username, password);
        }
    }
}