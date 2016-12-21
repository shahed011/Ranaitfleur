using System;
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

        public void AddDress(Item newItem)
        {
            _context.Add(newItem);
        }

        public IEnumerable<Item> GetAllDresses()
        {
            return _context.Items.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
