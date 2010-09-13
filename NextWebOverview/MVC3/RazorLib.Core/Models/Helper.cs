using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RazorLib.Core.Validation;

namespace RazorLib.Core.Models
{
    public class Helper : IValidatableObject
    {
        public Helper(User user)
        {

            Author = user;
            InitHelper();
        }

        public Helper()
        {
            InitHelper();
        }

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a helper name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
        public double Rating { get; set; }
        public HelperStatus Status { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CreatedDate is required")]
        [Date(ErrorMessage = "Please enter a valid CreatedDate")] //This is a custom attribute
        public DateTime CreatedDate { get; set; }

        //Implements IValidatableObject
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Status == HelperStatus.Draft && Rating > 0)
                yield return new ValidationResult("A Helper cannot have a non-zero rating when in draft status", 
                    new[] {"Status", "Rating"});
        }

        private void InitHelper()
        {
            Votes = new List<Vote>();
            Comments = new List<Comment>();
            Status = HelperStatus.Draft;
            Rating = 0;
        }
    }
}