using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World.API.Datas;
using World.API.Dto.CountryDto;
using World.API.Models;
using World.API.Repository;
using World.API.Repository.IRepository;

namespace World.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetAll()
        {
            var country =await  _countryRepository.GetAll();
            if(country==null)
            {
                return NoContent();
            }
            var countries = _mapper.Map<List<GetCountryDto>>(country);
            return Ok(countries);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetCountryDto>> GetById(int id)
        {
            var country = await _countryRepository.Get(id);
            if (country == null)
            {
                return NoContent();
            }
            var countries = _mapper.Map<GetCountryDto>(country);
            return Ok(countries);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto countryDto)
        {
            var result = _countryRepository.IsRecordExists(x=>x.Name==countryDto.Name);
            if(result)
            {
                return Conflict("Country already exists");
            }
            var country = _mapper.Map<Country>(countryDto);
            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new {id=country.Id},country);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<UpdateCountryDto>> Update(int id, [FromBody] UpdateCountryDto countryDto)
        {
            if(countryDto==null || countryDto.Id==0)
            {
                return BadRequest();
            }
            var country = _mapper.Map<Country>(countryDto);
            await _countryRepository.Update(country);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Country>> Delete(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var country = await _countryRepository.Get(id);
            if(country==null)
            {
                return NotFound();
            }
            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
