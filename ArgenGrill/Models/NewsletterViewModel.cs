using System;
using System.ComponentModel.DataAnnotations;

namespace ArgenGrill.Models
{
    public class NewsletterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }
    }
}