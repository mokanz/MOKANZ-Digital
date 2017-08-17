using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CustomerService : ICustomerService
    {
        Entities db = new Entities(); 

        public int Login(string username, string password) {

            int result;


            try
            {
                var customer = db.Customers
                    .Where(a => a.UserName == username && a.Password == password)
                    .FirstOrDefault();

                result = customer.CustomerID;
            }

            catch
            {
                OperationFault fault = new OperationFault();
                fault.Msg = "There was an error logging you in. Please try again.";
                throw new FaultException<OperationFault>(fault);
                
            }

            return result;
}

        //public string GetData(string username, string password)
        //{
        //    return string.Format("You entered");
        //}

        
    }

    [DataContract]
    public class OperationFault
    {
        [DataMember]
        public string Msg { get; set; }
        //[DataMember]
        //public int ErrorCode { get; set; }
    }
}
