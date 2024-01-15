using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThAmCoSystem.Areas.Identity.Data;

namespace ThAmCoSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerDashboardController : Controller
    {
        private readonly ThAmCoSystemContext _context;
        private readonly UserManager<ThAmCoSystemUser> _userManager;
        private readonly SignInManager<ThAmCoSystemUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomerDashboardController(ThAmCoSystemContext context, UserManager<ThAmCoSystemUser> userManager, SignInManager<ThAmCoSystemUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var product = _context.Products.ToList();

			var userId = _userManager.GetUserId(User);

			// Query the AccountBalance table based on the user's ID
			var accountBalance = _context.AccountBalances.SingleOrDefault(ab => ab.CustomerID == userId);

			if (accountBalance != null)
			{
				// If account balance is found, set it to ViewBag
				ViewBag.Balance = accountBalance.Balance;
			}
			return View(product);
        }




    }
}
