using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ranaitfleur.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ranaitfleur.Model
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var cart = session?.GetJson<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        public override void AddItem(Item product, int quantity, int size)
        {
            base.AddItem(product, quantity, size);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Item product, int size)
        {
            base.RemoveLine(product, size);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
