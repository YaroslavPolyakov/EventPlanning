using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [ForeignKey("Event")]
        public int? EventId { get; set; }
        public  bool ConfirmedEmail { get; set; }
        public string Token { get; set; }
        public Event Event { get; set; }
    }
}