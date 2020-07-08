using Microsoft.AspNet.Identity.EntityFramework;

namespace CollectionTrackerMVC.Models
{
    public class CollectionUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
