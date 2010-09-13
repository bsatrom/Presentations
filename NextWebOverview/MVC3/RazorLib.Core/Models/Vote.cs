using System;

namespace RazorLib.Core.Models
{
    public class Vote
    {
        public Vote()
        {
            Date = DateTime.Now;
        }

        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }
}