using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

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
        //[StringLength(450)]
        //[Index(IsUnique = true)]
        public string NameEvent { get; set; }
        [Required]
        public  string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}",ApplyFormatInEditMode = true)]
        [CustomDateRange(ErrorMessage = "Incorrectly input data")]
        public  DateTime? DateEvent { get; set; }
        [Required(ErrorMessage = "Incorrectly input time")]
        [DataType(DataType.Time)]
        public DateTime? TimeEvent { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}