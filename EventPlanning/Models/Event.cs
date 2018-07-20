using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class CustomDateRangeAttribute : RangeAttribute
    {
        public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.ToString(),
            DateTime.Now.AddYears(20).ToString()){  }
    }
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string NameEvent { get; set; }
        [Required]
        public  string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [CustomDateRange,]
        public  DateTime? DateEvent { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}