using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

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

        public IEnumerable<Sizes> GetAllSizes()
        {
            return _context.Sizes.ToList();
        }
    }
}
