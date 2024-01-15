

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCoSystem.Areas.Identity.Data;

namespace ThAmCoSystem.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class CustomersController : Controller
    {
        private readonly ThAmCoSystemContext _context;
        private readonly UserManager<ThAmCoSystemUser> _userManager;
        private readonly SignInManager<ThAmCoSystemUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public CustomersController(ThAmCoSystemContext context, UserManager<ThAmCoSystemUser> userManager, SignInManager<ThAmCoSystemUser> signInManager, RoleManager<IdentityRole> roleManager)
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

        public async Task<JsonResult> GetCustomers()
        {
            var customersInRole = await _userManager.GetUsersInRoleAsync("Customer");

            var customersData = customersInRole.Select(customer => new
            {
                FullName = customer.FullName,
                Id = customer.Id,
                PaymentAddress = customer.PaymentAddress,
                DeliveryAddress = customer.DeliveryAddress,
                Email = customer.Email,
                TelephoneNumber = customer.TelephoneNumber
            }).ToList();

            return Json(new { data = customersData });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _userManager.FindByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _userManager.DeleteAsync(customer);

                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Customer deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Error deleting customer." });
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle any specific exceptions related to database updates.
                return Json(new { success = false, message = "Error deleting customer. " + ex.Message });
            }
        }



    }
}
