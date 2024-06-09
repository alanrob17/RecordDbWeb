using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordDb.API.CustomActionFilters;
using RecordDb.API.Data;
using RecordDb.API.Models.Domain;
using RecordDb.API.Models.DTO;
using RecordDb.API.SQLRepository;
using Serilog.Data;
using System.Text.Json;

namespace RecordDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistRepository artistRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ArtistsController> logger;

        public ArtistsController(IArtistRepository artistRepository, IMapper mapper, ILogger<ArtistsController> logger)
        {
            this.artistRepository = artistRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: https://localhost:1234/api/artists?filterOn=field&filterQuery=recorded&sortOn=name&isAscending=true&pageNumber=1,pageSize=15
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            // GET data from the database - Domain Model
            var artists = await artistRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Return the DTO back to the client
            return Ok(mapper.Map<List<ArtistQueryDto>>(artists));
        }

        // GET: https://localhost:1234/api/artists/114
        [HttpGet]
        [Route("{id:int}")] 
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // GET Artist Domain mode from database
            var artist = await artistRepository.GetByIdAsync(id);

            if (artist == null)
            {
                return NotFound($"An Artist with Id: {id} wasn't found!");
            }

            logger.LogError("Bad error!");

            // Return the DTO back to the client 
            return Ok(mapper.Map<ArtistDto>(artist));
        }

        // POST: https://localhost:1234/api/artists
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddArtistDto addArtistDto)
        {
            // Map DTO to Domain Model
            var artist = mapper.Map<Artist>(addArtistDto);

            // Use Domain Model to create Artist
            artist = await artistRepository.CreateAsync(artist);

            // Map Domain model back to DTO
            var artistDto = mapper.Map<ArtistDto>(artist);

            return CreatedAtAction(nameof(GetById), new { id = artistDto.ArtistId }, artistDto);
        }

        // PUT: https://localhost:1234/api/artists/114
        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArtistDto updateArtistDto)
        {
            // Map DTO to Domain Model
            var artist = mapper.Map<Artist>(updateArtistDto);

            artist = await artistRepository.UpdateAsync(id, artist);

            if (artist == null)
            {
                return NotFound($"Artist with Id: {id} wasn't found!");
            }

            // Return the DTO back to the client 
            return Ok(mapper.Map<ArtistDto>(artist));
        }

        // DELETE: https://localhost:1234/api/artists/114
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var artist = await artistRepository.DeleteAsync(id);

            if (artist == null)
            {
                return NotFound($"Artist with Id: {id} not found!");
            }

            // Return the DTO back to the client 
            return Ok(mapper.Map<ArtistDto>(artist));
        }
    }
}
