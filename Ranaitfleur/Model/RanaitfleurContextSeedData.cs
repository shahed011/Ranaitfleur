using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ranaitfleur.Model
{
    public class RanaitfleurContextSeedData
    {
        private readonly RanaitfleurContext _context;
        private readonly UserManager<RanaitfleurUser> _userManager;

        public RanaitfleurContextSeedData(RanaitfleurContext context, UserManager<RanaitfleurUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if (await _userManager.FindByEmailAsync("shahed@ranaitfleur.com") == null)
            {
                var user = new RanaitfleurUser
                {
                    UserName = "Shahed",
                    Email = "shahed@ranaitfleur.com",
                    LockoutEnabled = false
                };

                await _userManager.CreateAsync(user, "#Compaq#11#");
            }

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

                item = new Item
                {
                    ItemType = 2,
                    Name = "Bustier Pearl Detailed Long Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/DemiCouture/BustierPearlDetailedLongDress/1.jpg,~/img/DemiCouture/BustierPearlDetailedLongDress/2.jpg,~/img/DemiCouture/BustierPearlDetailedLongDress/3.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Discotheque Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/DemiCouture/DiscothequeDress/1.jpg,~/img/DemiCouture/DiscothequeDress/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Pink Fish Tale Gown",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/DemiCouture/PinkFishTaleGown/1.jpg,~/img/DemiCouture/PinkFishTaleGown/2.jpg,~/img/DemiCouture/PinkFishTaleGown/3.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Princess Pearl Jumpsuit",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/DemiCouture/PrincessPearlJumpsuit/1.jpg,~/img/DemiCouture/PrincessPearlJumpsuit/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Princess Pink Long Skirt",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/DemiCouture/PrincessPinkLongSkirt/1.jpg,~/img/DemiCouture/PrincessPinkLongSkirt/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Sapphire Lace Pencil Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/DemiCouture/SapphireLacePencilDress/1.jpg,~/img/DemiCouture/SapphireLacePencilDress/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 3,
                    Name = "Black Crushed Velvet Slip With Pearl Bead Straps",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/BlackCrushedVelvetSlipWithPearlBeadStraps/1.jpg,~/img/ReadyToWear/BlackCrushedVelvetSlipWithPearlBeadStraps/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 3,
                    Name = "Black V Neck Mini Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/BlackVNeckMiniDress/1.jpg,~/img/ReadyToWear/BlackVNeckMiniDress/2.jpg,~/img/ReadyToWear/BlackVNeckMiniDress/3.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 3,
                    Name = "Blue Silk Slip Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/BlueSilkSlipDress/1.jpg,~/img/ReadyToWear/BlueSilkSlipDress/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 3,
                    Name = "Gold Chain Backless Gown",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/GoldChainBacklessGown/1.jpg,~/img/ReadyToWear/GoldChainBacklessGown/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 3,
                    Name = "Golden Vegas Neck Mini Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/GoldenVegasNeckMiniDress/1.jpg,~/img/ReadyToWear/GoldenVegasNeckMiniDress/2.jpg,~/img/ReadyToWear/GoldenVegasNeckMiniDress/3.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 3,
                    Name = "Thick Strap Backless Gown",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/ThickStrapBacklessGown/1.jpg,~/img/ReadyToWear/ThickStrapBacklessGown/2.jpg",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}