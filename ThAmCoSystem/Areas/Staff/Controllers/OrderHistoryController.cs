using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThAmCoSystem.Areas.Identity.Data;

namespace ThAmCoSystem.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class OrderHistoryController : Controller
    {
        private readonly ThAmCoSystemContext _context;
        private readonly UserManager<ThAmCoSystemUser> _userManager;
        private readonly SignInManager<ThAmCoSystemUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public OrderHistoryController(ThAmCoSystemContext context, UserManager<ThAmCoSystemUser> userManager, SignInManager<ThAmCoSystemUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

		public IActionResult Index()
		{
			 

			return View();
		}

		public JsonResult OrderHistory()
		{

			var data = _context.OrdersHistory
				.Select(o => new
				{
					OrderId = o.OrderId,
					CustomerId = o.CustomerId,
					CustomerFullName = _userManager.Users
				.Where(u => u.Id == o.CustomerId)
				.Select(u => u.FullName)
				.FirstOrDefault(),

					Status = o.Status,
					ProductName = o.Order.OrderItems.FirstOrDefault().Product.Name,
					Price = o.Order.OrderItems.FirstOrDefault().Product.Price,
				})
				.ToList();
			return Json(new { data });
		}


	}
}
