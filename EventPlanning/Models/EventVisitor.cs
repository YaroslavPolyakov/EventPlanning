using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class EventVisitor
    {
        public Event Events { get; set; }
        public IEnumerable<Visitor> Visitors { get; set; }
    }
}