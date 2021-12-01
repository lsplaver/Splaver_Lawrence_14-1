using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContactManager.Models.Validation;

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
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "Phone number must be 14 characters long including parenthesis and dashes.")]
        [CustomPhoneFormat(ErrorMessage = "Phone number must be in the '(123)-456-7890' format including parenthesis and dashes.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [CustomEmailFormat(ErrorMessage = "Email must e in the format of 'name@example.com'.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a category.")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public string Organization { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.Replace(' ', '-');
    }
}
