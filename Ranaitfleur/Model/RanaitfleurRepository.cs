using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Ranaitfleur.Model
{
    public class RanaitfleurRepository : IRanaitfleurRepository
    {
        private readonly RanaitfleurContext _context;
        private ILogger<RanaitfleurRepository> _logger;

        public RanaitfleurRepository(RanaitfleurContext context, ILogger<RanaitfleurRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Item> GetAllDresses()
        {
            return _context.Items.ToList();
        }

        public IEnumerable<Subscribers> GetAllSubscribers()
        {
            return _context.Subscriberses.ToList();
        }

        public void AddDress(Item newItem)
        {
            _context.Add(newItem);
        }

        public void AddSubscriber(Subscribers newSubscriber)
        {
            _context.Add(newSubscriber);
        }

        public bool RemoveSubscriber(string email)
        {
            var subscriberToRemove = _context.Subscriberses.FirstOrDefault(s => s.Email.Equals(email));
            if (subscriberToRemove == null) return false;

            _context.Remove(subscriberToRemove);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
