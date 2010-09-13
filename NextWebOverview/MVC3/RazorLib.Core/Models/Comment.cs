using System;

namespace RazorLib.Core.Models
{
    public class Comment
    {
        public Comment()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}