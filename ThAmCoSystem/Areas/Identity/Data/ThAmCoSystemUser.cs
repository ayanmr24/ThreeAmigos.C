using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ThAmCoSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ThAmCoSystemUser class
public class ThAmCoSystemUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? PaymentAddress { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? TelephoneNumber { get; set; }

}

