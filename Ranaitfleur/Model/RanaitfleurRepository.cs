using System.Collections.Generic;
using System.Linq;

namespace Ranaitfleur.Model
{
    public class RanaitfleurRepository : IRanaitfleurRepository
    {
        private readonly RanaitfleurContext _context;

        public RanaitfleurRepository(RanaitfleurContext context)
        {
            _context = context;
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
