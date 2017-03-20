using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ranaitfleur.Model
{
    public class RanaitfleurContextSeedData
    {
        private readonly RanaitfleurContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RanaitfleurContextSeedData(RanaitfleurContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedData()
        {
            if (await _userManager.FindByEmailAsync("shahed@ranaitfleur.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "Shahed",
                    Email = "shahed@ranaitfleur.com",
                    LockoutEnabled = false
                };

                await _userManager.CreateAsync(user, "#Compaq#11#");

                if (!_roleManager.RoleExistsAsync("Administrator").Result)
                {
                    var role = new IdentityRole { Name = "Administrator" };
                    await _roleManager.CreateAsync(role);
                }

                _userManager.AddToRoleAsync(user, "Administrator").Wait();
            }

            if (!_context.Items.Any())
            {
                var item = new Item
                {
                    ItemType = 1,
                    Name = "Fully Bespoke",
                    NoOfItemInStock = 0,
                    Price = 75,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 1,
                    Name = "Made To Measure",
                    NoOfItemInStock = 0,
                    Price = 75,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "",
                    Description1 = "",
                    Description2 = ""
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Black Crushed Velvet Slip With Pearl Bead Straps",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/BlackCrushedVelvetSlipWithPearlBeadStraps/1.jpg,~/img/ReadyToWear/BlackCrushedVelvetSlipWithPearlBeadStraps/2.jpg",
                    Description1 = "Oozing a visual luxury that is sheer opulence, this crushed velvet mini dress with subtle linear design is the perfect dress for feeling fashion confident. Thoughtfully cut to enhance the figure, the front and back strapping is a symphony of pearls and beads that create a look with more than a subtle nod to the Victorian era.",
                    Description2 = "Combining vintage and contemporary, the dress is a sheer joy to wear with a look that will be admired from any angle."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Black V Neck Mini Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/BlackVNeckMiniDress/1.jpg,~/img/ReadyToWear/BlackVNeckMiniDress/2.jpg,~/img/ReadyToWear/BlackVNeckMiniDress/3.jpg",
                    Description1 = @"It’s time for you to rock your fashion look with a contemporary mini-dress that is bursting with personality. 
                                    Made from a luxury PVC vinyl fabric, the dress has been thoughtfully designed to ensure that style 
                                    and fit mean that you look your radiant best from every angle.",
                    Description2 = @"From the V-shaped bust-line and vertical front tucks to the demi-pinch waist and concealed side zip, 
                                    everything purposely comes together to create a seamless look of daring elegance."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Gold Chain Backless Gown",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/GoldChainBacklessGown/1.jpg,~/img/ReadyToWear/GoldChainBacklessGown/2.jpg",
                    Description1 = @"Recreate an era where glamour and elegance were in vogue, with a dress that exudes daring sophistication.",
                    Description2 = @"Made from the highest quality silk in a vibrant blue, this head-turning gown has been designed for 
                                    visual impact. The front swoops to the side in order to add interest without dominating, with the 24- 
                                    carat gold plated chain straps undoubtedly being the star feature. Leading to a back reveal that 
                                    portrays your femininity, this is a dress that makes you feel like you deserve only the very best."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Blue Silk Slip Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/BlueSilkSlipDress/1.jpg,~/img/ReadyToWear/BlueSilkSlipDress/2.jpg",
                    Description1 = @"Refined elegance is not always about intricate detail, with the beauty in this dress exuding from its seamless simplicity. 
                                    Made from the highest quality silk and styled in an exquisite sapphire blue,
                                    this is a dress that knows how to flatter,
                                    with its shaped pinch waist,
                                    raised back neckline and V - plunge frontage.",
                    Description2 = @"Comfort ensues courtesy of its ability to simply slip over the head,
                                    proving that classic clean lines can create contemporary fashion supremacy."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Thick Strap Backless Gown",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/ThickStrapBacklessGown/1.jpg,~/img/ReadyToWear/ThickStrapBacklessGown/2.jpg",
                    Description1 = @"Achieve that enviable flow as you walk, making heads turn and allowing your inner confidence to be 
                                    set free. Made from the highest quality silk, this long gown is designed with sheer elegance in mind 
                                    with absolutely no compromise on design.",
                    Description2 = @"Swathes of silk are shaped into a demi-sweetheart neckline with a side tuck feature slimming the 
                                    silhouette. The back is a genius touch of femininity, combining wearability with a back reveal for a 
                                    look that impresses from each and every angle."
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
                    Description1 = "Accentuate your femininity with a Bustier dress that brings back the opulence of vintage luxe dressing. This full - length dress has an admirable attention to detail, starting with a bustier top leading a flattering defined waistband.Shaped panels at the thigh highlight the fishtail element to the dress with a lower swish of fabric creating ease of movement.",
                    Description2 = "The back is a myriad of exquisite detail, with pearl decoration leading to the perfect fishtail feature, ensuring turning head admiration. The look is further enhanced with slender pearl straps that are designed to add feminine delicacy to the overall feel."
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
                    Description1 = @"Capture the elegance of luxury opulent dressing with a dress that is almost too beautiful for words. 
                                    Made from the highest grade of silk, there is simply no compromising on the amount of material 
                                    used, with the design respecting shape and wearability in equal measure. The carefully shaped bust-line 
                                    leads to ruches of silk around the waist, accentuating the figure to both the front and the back.",
                    Description2 = @"Add to this the pleat effect fishtail and you have a gown that is worthy of accolades, ensuring that 
                                    when wearing it, you are guaranteed to be complimented and admired for your superior sense of style."
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
                    ImagePath = "~/img/ReadyToWear/PrincessPinkLongSkirt/1.jpg,~/img/ReadyToWear/PrincessPinkLongSkirt/2.jpg",
                    Description1 = "Embrace the beauty of femininity with a long-length skirt that is worthy of any Princess. With a fantastical, luxury flow that allows for superior freedom of movement, this is a skirt that will flatter the figure with its defined waistband pinching the waist.",
                    Description2 = "Delicate pleats follow through to the fullness of the skirt, creating the look of your childhood dreams brought to a blissful level of modern exquisiteness."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Duchess Royal",
                    NoOfItemInStock = 5,
                    Price = 2500,
                    Weight = 0.95f,
                    Dimentions = "30 x 20 x 20",
                    ImagePath = "~/img//ReadyToWear/DuchessRoyal/1.jpg",
                    Description1 = @"With a fluidity of movement that makes it an utter joy to wear, this matt silk dress is the epitome of 
                                    classy dressing. Cut to ensure maximum flow and movement upon every footstep, this is a dress that 
                                    respects the feminine form and will flatter and slim.",
                    Description2 = @"A deep plunge frontage is the height of delicacy, with the dress skimming the hips and leading to a 
                                    shaped hemline that kisses the calves and ankles. 
                                    The resultant look is elegance redefined, making you feel wholly feminine and spoilt by such a luxe fabric choice."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Golden Vegas Neck Mini Dress",
                    NoOfItemInStock = 5,
                    Price = 1000,
                    Weight = 0.0f,
                    Dimentions = "",
                    ImagePath = "~/img/ReadyToWear/GoldenVegasNeckMiniDress/1.jpg,~/img/ReadyToWear/GoldenVegasNeckMiniDress/2.jpg,~/img/ReadyToWear/GoldenVegasNeckMiniDress/3.jpg",
                    Description1 = @"Embrace the vibrancy of life with this stunning mini-dress that exudes character. 
                                    Made from a luxury PVC vinyl fabric, the dress is shaped to accentuate the figure, with the material 
                                    allowing freedom of movement and ensuring a flattering fit.",
                    Description2 = @"Simple in shape but with an overall look that is designed to radiate your individual sense of style, 
                                    wearing this dress adds confidence, ensuring that you are noticed and admired with every step you take."
                };
                _context.Items.Add(item);

                item = new Item
                {
                    ItemType = 2,
                    Name = "Discotheque Dreams",
                    NoOfItemInStock = 5,
                    Price = 2500,
                    Weight = 0.2f,
                    Dimentions = "30 x 20 x 20",
                    ImagePath = "~/img/DemiCouture/DiscothequeDreams/1.jpg",
                    Description1 = "This Statement-making mini dress is topped by 24 carat gold chain straps, dripping in white sequins, soft cotton lining means you can party all night!  A body-skimming piece is crafted here in London with a fun mini skirt cut, allowing for a comfortable, flexible fit.",
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
                    Description1 = @"With an attention to detail that seems incredible to achieve, this couture mini-length dress is most 
                                    definitely designed to dazzle. A myriad of large white sequins are stitched perfectly in 
                                    place in order to create movement and capture the light. The emphasis is on movement and 
                                    wearability, with the dress accentuating the figure whilst still allowing you to dance the night away.",
                    Description2 = @"The look is finished with bold 24-carat plated chained straps, creating a dress that when worn will 
                                    create memories of a night when you were most definitely in the limelight."
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
                    Description1 = @"Delicate yet striking, this shorter-length made to order dress is an exquisite array of fineness, 
                                    combining detail that is designed to make you feel beautiful to the optimum level of perfection.",
                    Description2 = @"The reveal lace upper is gently scattered with pearls and sapphires to create a look of refined 
                                    innocence. Balanced by the high-waisted skirt that flows seamlessly from the top to create a 
                                    masterpiece, this is the dress that you dreamt about made into reality."
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
                    Description1 = "Indulge in a lace body whose sheer exquisiteness is a beauty to behold. The lace has a floral patterning that is thoughtfully replicated with precious stone floral embellishment, creating a look that is delicately breath - taking. The body is further enhanced with fresh water pearl trimming to both the neckline and leg cuffs, with a soft scoop back completing the look of refined femininity.",
                    Description2 = "A luxury item that is designer to be admired, this is flawless fashion for the discerning."
                };
                _context.Items.Add(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}