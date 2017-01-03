using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ranaitfleur.Model
{
    public interface IRanaitfleurRepository
    {
        IEnumerable<Item> GetAllDresses();
        IEnumerable<Subscribers> GetAllSubscribers();

        void AddDress(Item newItem);
        void AddSubscriber(Subscribers newSubscriber);

        Task<bool> SaveChangesAsync();
    }
}