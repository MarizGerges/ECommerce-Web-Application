namespace Talabat.Core.Entities.Identity
{
    public class Address 
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string lastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string UserId { get; set; } // foreign key 
        public AppUser User { get; set; }  // NAV prop [one]

    }
}