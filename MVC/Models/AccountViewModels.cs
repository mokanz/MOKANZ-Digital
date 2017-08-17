using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOKANZ.Models
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class CustomerModel {

        [Required]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "{0} can only contain alphabetic characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "{0} can only contain alphabetic characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [RegularExpression("^[a-z]{3,18}", ErrorMessage = "{0} must start with a letter, and must be between 3 and 18 characters.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class RegisterViewModel{

        public CustomerModel Customer { get; set; }

        
        public Address Address { get; set; }

        
        public Contact Contact { get; set; }

        [Display(Name = "Contact Type")]
        public IEnumerable<ContactTypeModel> ContactType { get; set; }
        

        [Display(Name = "Address Type")]
        public IEnumerable<AddressTypeModel> AddressType { get; set; }


    }


    public class CategoryModel
    {
        public string CategoryName { get; set; }
        public string CategoryID { get; set; }
        public string ParentCategory { get; set; }
    }

    public class AddressTypeModel
    {
        public string AddressType { get; set; }
        public int AddressTypeID { get; set; }
    }


    public class ContactTypeModel
    {
        public int ContactTypeID { get; set; }
        public string ContactType { get; set; }
    }
}