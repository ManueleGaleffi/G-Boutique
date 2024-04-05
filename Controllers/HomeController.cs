using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using ecommerce.Areas.Identity.Data;
using System.Collections.Generic;
using System.Linq;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ecommerceIdentityDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ecommerceIdentityDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            ViewBag.IsAuthenticated = isAuthenticated;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        // Metodo per visualizzare la pagina del carrello
        public IActionResult Cart()
        {
            var cartItems = GetCartItemsFromDatabase();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var userId = _userManager.GetUserId(User);

            // Cerca nel carrello dell'utente se esiste già un elemento per il prodotto
            var cartItem = _context.Cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cartItem = new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = 1
                };
                _context.Cart.Add(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        // Metodo per rimuovere un elemento dal carrello
        public IActionResult RemoveFromCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            var cartItem = _context.Cart.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                _context.Cart.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        // Metodo per completare l'ordine
        [HttpPost]
        public IActionResult CompleteOrder()
        {
            // Qui puoi implementare la logica per completare l'ordine
            // Ad esempio, potresti svuotare il carrello o inviare una mail di conferma

            // Svuota il carrello
            EmptyCart();

            // Dopo aver completato l'ordine, reindirizza l'utente alla pagina di ringraziamento
            TempData["OrderCompleteMessage"] = "Grazie per il tuo ordine! Ti arriverà una mail di conferma entro 24 ore.";
            return RedirectToAction("OrderComplete");
        }

        // Metodo per visualizzare la pagina di ringraziamento dopo aver completato l'ordine
        public IActionResult OrderComplete()
        {
            // Mostra un messaggio di ringraziamento all'utente
            ViewBag.OrderCompleteMessage = TempData["OrderCompleteMessage"] as string;
            return View();
        }

        public IActionResult Chisiamo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Metodo di supporto per ottenere i dettagli dei prodotti nel carrello dal database
        private List<Cart> GetCartItemsFromDatabase()
        {
            // Ottieni l'ID dell'utente attualmente loggato
            var userId = _userManager.GetUserId(User);

            // Assicurati che _context.Carts sia un DbSet<Cart> o una IQueryable<Cart> che supporta il metodo Where
            var cartItems = _context.Cart
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToList();

            return cartItems;
        }

        // Metodo per svuotare il carrello
        private void EmptyCart()
        {
            // Ottieni l'ID dell'utente attualmente loggato
            var userId = _userManager.GetUserId(User);

            // Ottieni tutti gli elementi nel carrello per l'utente corrente
            var cartItems = _context.Cart.Where(c => c.UserId == userId).ToList();

            // Rimuovi tutti gli elementi dal carrello
            _context.Cart.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        // Metodo di supporto per ottenere un prodotto dall'ID (simulazione)
        private Product GetProductById(int productId)
        {
            if (productId == 1)
            {
                return new Product { ProductId = 1, Name = "MONCLER X PALM ANGELS GENIUS PARKA", Price = 1890 };
            }
            else if (productId == 2)
            {
                return new Product { ProductId = 2, Name = "GIACCA SPORTIVA PALME", Price = 595 };
            }
            else if (productId == 3)
            {
                return new Product { ProductId = 3, Name = "Moncler x Palm Angels Genius Piumino", Price = 1290 };
            }
            else if (productId == 4)
            {
                return new Product { ProductId = 4, Name = "MONCLER X PALM ANGELS GENIUS GILET IMBOTTITO", Price = 990 };
            }
            else if (productId == 5)
            {
                return new Product { ProductId = 5, Name = "GIACCA-CAMICIA CON LOGO", Price = 845 };
            }
            else if (productId == 6)
            {
                return new Product { ProductId = 6, Name = "FELPA CON CAPPUCCIO FOGGY PA", Price = 495 };
            }
            else if (productId == 7)
            {
                return new Product { ProductId = 7, Name = "SLIDES CON MONOGRAM", Price = 225 };
            }
            else if (productId == 8)
            {
                return new Product { ProductId = 8, Name = "T-SHIRT TOKYO CON EFFETTO VERNICE", Price = 225 };
            }
            else if (productId == 9)
            {
                return new Product { ProductId = 9, Name = "T-SHIRT CLASSICA SHARK", Price = 280 };
            }
            else if (productId == 10)
            {
                return new Product { ProductId = 10, Name = "PALM ANGELS MONCLER MAYA PIUMINO", Price = 4500 };
            }
            else if (productId == 11)
            {
                return new Product { ProductId = 11, Name = "PANTALONI SPORTIVI CON LOGO", Price = 435 };
            }
            else if (productId == 12)
            {
                return new Product { ProductId = 12, Name = "FELPA CON CAPPUCCIO CON LOGO", Price = 435 };
            }
            else if (productId == 13)
            {
                return new Product { ProductId = 13, Name = "PANTALONI SPORTIVI CON MONOGRAM", Price = 435 };
            }
            else if (productId == 14)
            {
                return new Product { ProductId = 14, Name = "FELPA GIROCOLLO TIE DYE DICE GAME", Price = 495 };
            }
            else if (productId == 15)
            {
                return new Product { ProductId = 15, Name = "CARDIGAN CON LOGO", Price = 690 };
            }
            else
            {
                return null; // Se l'ID non corrisponde a nessun prodotto
            }
        }
    }
}
