using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone(ErrorMessage = "Not a valid phone number.")]
        public /*PhoneAttribute*/ string Phone { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Not a valid email address.")]
        public /*EmailAddressAttribute*/ string Email { get; set; }

        [Required(ErrorMessage = "Please enter a category.")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public string Organization { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
