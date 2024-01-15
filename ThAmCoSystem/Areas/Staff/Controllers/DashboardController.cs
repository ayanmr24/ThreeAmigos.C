using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThAmCoSystem.Areas.Identity.Data;

namespace ThAmCoSystem.Areas.Staff.Controllers
{
    [Area("Staff")]
	public class DashboardController : Controller
	{
        private readonly ThAmCoSystemContext _context;
        private readonly UserManager<ThAmCoSystemUser> _userManager;
        private readonly SignInManager<ThAmCoSystemUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(ThAmCoSystemContext context, UserManager<ThAmCoSystemUser> userManager, SignInManager<ThAmCoSystemUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		public async Task<IActionResult> Index()
		{
			// Order count Details 

			var customersInRole = await _userManager.GetUsersInRoleAsync("Customer");
			int customerCount = customersInRole.Count;            // Get the count of users in the "Customer" role
			ViewBag.CountCustomer = customerCount;            // Store the count in the ViewBag to be used in the view


			// Order count Details 

			var countRole = _context.Orders.ToList();
			int countOrder = countRole.Count;
			ViewBag.OrderCount = countOrder;

			var productList = _context.Products.ToList();
			int ProsuctCount = productList.Count;
			ViewBag.CountProduct = ProsuctCount;


			return View();
		}




	}
}