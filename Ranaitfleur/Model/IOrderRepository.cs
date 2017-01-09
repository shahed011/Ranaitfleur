using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ranaitfleur.Model
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        Task<bool> SaveOrder(Order order);
    }
}