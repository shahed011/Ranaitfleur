using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ranaitfleur.Model
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RanaitfleurContext _context;
        private ILogger<RanaitfleurRepository> _logger;

        public OrderRepository(RanaitfleurContext context, ILogger<RanaitfleurRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Order> Orders => _context.Orders.Include(o => o.Lines).ThenInclude(l => l.Item);

        public async Task<bool> SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Item));
            if (order.OrderId == 0)
            {
                _context.Orders.Add(order);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public Task<Order> GetOrder(int orderId)
        {
            return _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}