using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faceplant.Core.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
