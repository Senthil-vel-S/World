using System.ComponentModel.DataAnnotations;

namespace World.API.Dto.CountryDto
{
    public class GetCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CountryCode { get; set; }
    }
}
