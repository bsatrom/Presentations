using System;

namespace RazorLib.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri Gravatar { get; set; }
    }
}