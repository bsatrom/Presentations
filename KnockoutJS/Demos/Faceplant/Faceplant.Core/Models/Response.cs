using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faceplant.Core.Models
{
    public class Response
    {
        public int Id { get; set; }
        public virtual Session Session { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }            
    }
}
