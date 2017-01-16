using System.Collections.Generic;
using System.Linq;

namespace Ranaitfleur.Model
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem(Item item, int quantity, int size)
        {
            var line = _lineCollection
                .FirstOrDefault(p => p.Item.Id == item.Id && p.Size == size);

            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Item = item,
                    Quantity = quantity,
                    Size = size
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Item item, int size) =>
            _lineCollection.RemoveAll(l => l.Item.Id == item.Id && l.Size == size);

        public virtual decimal ComputeTotalValue() =>
            _lineCollection.Sum(e => e.Item.Price * e.Quantity);

        public virtual void Clear() => _lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => _lineCollection;
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
    }
}
