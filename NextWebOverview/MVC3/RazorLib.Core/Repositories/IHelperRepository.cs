using System.Collections.Generic;
using RazorLib.Core.Models;

namespace RazorLib.Core.Repositories
{
    public interface IHelperRepository
    {
        IEnumerable<Helper> GetAll();
        Helper Get(int id);
        void Save(Helper helper);
        void Delete(Helper helper);
    }
}