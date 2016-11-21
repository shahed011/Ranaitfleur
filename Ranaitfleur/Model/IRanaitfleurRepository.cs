using System.Collections.Generic;

namespace Ranaitfleur.Model
{
    public interface IRanaitfleurRepository
    {
        IEnumerable<Item> GetAllDresses();
        IEnumerable<Sizes> GetAllSizes();
    }
}