using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using ThAmCoSystem.Areas.Identity.Data;
using ThAmCoSystem.Models.OrderItems;
using ThAmCoSystem.Models.Orders;
using ThAmCoSystem.Models.OrdersHistory;

namespace ThAmCoSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ThAmCoSystemContext _context;
        private readonly UserManager<ThAmCoSystemUser> _userManager;
        private readonly SignInManager<ThAmCoSystemUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration; // Add this field

        public OrderController(ThAmCoSystemContext context, UserManager<ThAmCoSystemUser> userManager, SignInManager<ThAmCoSystemUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public IActionResult PlaceOrder(int[] productIds)
		{
			// Get the currently logged-in user
			var currentUser = _userManager.GetUserAsync(User).Result;

			// Retrieve the products selected for the order
			var products = _context.Products.Where(p => productIds.Contains(p.ProductId)).ToList();

			// Calculate total price of the selected products
			var totalPrice = products.Sum(p => p.Price);

			// Retrieve the user's account balance
			var userBalance = _context.AccountBalances.FirstOrDefault(a => a.CustomerID == currentUser.Id);

			// Check if the user has sufficient funds
			if (userBalance.Balance < totalPrice)
			{
				// Handle insufficient funds (redirect, show an error message, etc.)
				return RedirectToAction("InsufficientFunds");
			}

			// Create a new order
			var order = new Order
			{
				OrderDate = DateTime.Now,
				Status = "Pending", // You can set the initial status as needed
				CustomerID = currentUser.Id,
				ThAmCoSystemUser = currentUser,
				OrderItems = products.Select(p => new OrderItem
				{
					Quantity = 1, // You may adjust the quantity based on your requirements
					UnitPrice = p.Price,
					Product = p
				}).ToList()
			};

			// Update user's account balance
			userBalance.Balance -= totalPrice;

			// Update product stock
			foreach (var product in products)
			{
				product.Stock -= 1; // Assuming you're decrementing the stock by 1 for each purchased item
			}

			// Save changes to the database
			_context.Orders.Add(order);
			_context.SaveChanges();

			// Create an OrderHistory entry
			var orderHistory = new OrderHistory
			{
				CustomerId = currentUser.Id,
 				OrderId = order.OrderId,
				Order = order,
				Status = order.Status
			};

			// Save OrderHistory to the database
			_context.OrdersHistory.Add(orderHistory);
			_context.SaveChanges();


			TempData["Success"] = "Your order has been placed successfully. Thank you for shopping with us!";

			//		SendOrderConfirmationEmail(currentUser.Email, order);

			// Redirect or return a success view
			return View();
		}
		 



	}
}
