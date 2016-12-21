using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranaitfleur.Model
{
    public class RanaitfleurContextSeedData
    {
        private RanaitfleurContext _context;

        public RanaitfleurContextSeedData(RanaitfleurContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Items.Any())
            {
                var item = new Item
                {
                    ItemType = 1,
                    Name = "Discotheque Dreams",
                    NoOfItemInStock = 5,
                    Price = 2500,
                    Weight = 0.2f,
                    Dimentions = "30 x 20 x 20",
                    ImagePath = "~/img/DiscothequeDreams/DiscothequeDreams.jpg",
                    Description1 = "This Statement-making mini dress is topped by 24 carat gold chain straps, dripping in white sequins, soft cotton lining means you can party all night!  A body-skimming piece is crafted here in London with a fun mini skirt cut, allowing for a comfortable, flexible fit.",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 1,
                    Name = "Duchess Royal",
                    NoOfItemInStock = 5,
                    Price = 2500,
                    Weight = 0.95f,
                    Dimentions = "30 x 20 x 20",
                    ImagePath = "~/img/DuchessRoyal/DuchessRoyal.jpg",
                    Description1 = "This  elegant gown redefines femininity, hand crafted  in decadent habotai silk with a flattering deep V meeting at a snitched waist band, elongating the leg the gown flows from the waist giving the silk a lustrous fluid shape.",
                    Description2 = "Model Wears UK size 8, slightly tighter on the waist. Recommend taking one size bigger."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 1,
                    Name = "Gold Chain Gown",
                    NoOfItemInStock = 5,
                    Price = 2500,
                    Weight = 0.95f,
                    Dimentions = "30 x 20 x 20",
                    ImagePath = "~/img/GoldChainGown/GoldChainGown.jpg,~/img/GoldChainGown/GoldChainGown1.jpg,~/img/GoldChainGown/GoldChainGown2.jpg,~/img/GoldChainGown/GoldChainGown3.jpg,~/img/GoldChainGown/GoldChainGown4.jpg",
                    Description1 = "This stunning gown is shown in royal blue silk loose fitting allowing for lots of movement high cut to the hip bone, backless though decorated and complimented with delicate 24 carrot gold chain straps.",
                    Description2 = "Model Wears UK size 8 as a loose fit, slightly loose fitted on the waist. Recommend taking one size smaller depending on desired fit."
                };
                _context.Items.Add(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}