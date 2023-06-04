using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.DTOs
{
    public class AddressDTO
    {

        [Required]
        public string FristName { get; set; }
        [Required]

        public string lastName { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Street { get; set; }
    }
}
