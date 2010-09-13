using System.Collections.Generic;
using System.Linq;
using RazorLib.Core.Models;
using RazorLib.Core.Repositories;

namespace RazorLib.Infrastructure.Repositories
{
    public class HelperRepository: IHelperRepository
    {
        private static IList<Helper> _helpers;

        public HelperRepository()
        {
            _helpers = new List<Helper>
                           {
                               new Helper
                                   {
                                       Id = 5,
                                       Author = new User { Name = "Bob Stevens", Email = "bstevens@gmail.com" },
                                       Name = "Helper Simple",
                                       Description = "This is a simple helper",
                                       Rating = 4.7d,
                                       Status = HelperStatus.Published
                                   },
                                new Helper
                                   {
                                       Id = 1,
                                       Author = new User { Name = "Ed Haskel", Email = "ehaskel@live.com" },
                                       Name = "Helper Complex",
                                       Description = "This one is a bit more complex",
                                       Rating = 3.3d,
                                       Status = HelperStatus.Published
                                   },
                                new Helper
                                   {
                                       Id = 12,
                                       Author = new User { Name = "Brian Moore", Email = "brimoore@apple.com" },
                                       Name = "Snippet for SO",
                                       Description = "This is just a simple snippet",
                                       Rating = 1.9d,
                                       Status = HelperStatus.Published
                                   },
                           };
        }

        public IEnumerable<Helper> GetAll()
        {
            return _helpers;
        }

        public Helper Get(int id)
        {
            return _helpers.SingleOrDefault(h => h.Id == id);
        }

        public void Save(Helper helper)
        {
            _helpers.Add(helper);
        }

        public void Delete(Helper helper)
        {
            _helpers.Remove(helper);
        }
    }
}