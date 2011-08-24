using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Faceplant.Core.Models
{
    public class Session
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public string Location { get; set; }
        
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<Response> Responses { get; set; }

        //One-To-Many
        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }

        public virtual ICollection<int> Ratings { get; set; }
        
        public double AverageRating()
        {
            return Ratings.Count > 0 ? Ratings.Average() : 0;
        }

    }
}
