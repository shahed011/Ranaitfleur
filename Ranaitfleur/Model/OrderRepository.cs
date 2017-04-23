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

        public IEnumerable<Order> Orders => _context.Orders.Include(o => o.Lines);//.ThenInclude(l => l.Item);

        public async Task<bool> SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                //_context.AttachRange(order.Lines.Select(l => l.Item));
                _context.Orders.Add(order);
            }
            else
            {
                _context.Orders.Update(order);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public Task<Order> GetOrder(int orderId)
        {
            return _context.Orders.Include(o => o.Lines).FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public Task<List<Order>> GetOrdersByUserName(string userName)
        {
            return _context.Orders.Where(o => o.UserName == userName).Include(o => o.Lines).ToListAsync();
        }

        public Task<List<Order>> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Lines).ToListAsync();
        }

        public async Task RemoveIncompleteOrders()
        {
            var ordersToRemove = _context.Orders.Where(o => o.Status == OrderStatus.Incomplete).Include(o => o.Lines);
            _context.Orders.RemoveRange(ordersToRemove);
            await _context.SaveChangesAsync();
        }
    }
}