using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ranaitfleur.Model
{
    public interface IRanaitfleurRepository
    {
        IEnumerable<Item> GetAllDresses();
        void AddDress(Item newItem);
        Task<bool> SaveChangesAsync();
    }
}