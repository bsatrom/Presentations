using System.Collections.Generic;
using System.Linq;

namespace Faceplant.Core.Models
{
    //Need to update SpeakerForm
    public class Speaker
    {
        public int SpeakerId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string TwitterHandle { get; set; }
        public string[] Languages { get; set; }
        public string[] FavoriteTopics { get; set; }
        public string State { get; set; }
        public string PhotoUrl { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
        
        public double AverageRating()
        {
            return Sessions.Count > 0 
                ? Sessions.Average(s => s.Ratings != null 
                    ? s.Ratings.Average() : 0) 
                : 0;
        }
    }
}
