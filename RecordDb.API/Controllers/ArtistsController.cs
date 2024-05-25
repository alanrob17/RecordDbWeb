using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordDb.API.Data;
using RecordDb.API.Models.Domain;
using RecordDb.API.Models.DTO;
using RecordDb.API.SQLRepository;

namespace RecordDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistRepository artistRepository;

        public ArtistsController(IArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        // GET: https://localhost:1234/api/artists
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // GET data from the database - Domain Model
            var artists = await artistRepository.GetAllAsync();

            // MAP Domain Model to DTO
            var artistsDto = new List<ArtistDto>();

            foreach (var artist in artists)
            {
                artistsDto.Add(new ArtistDto()
                {
                    ArtistId = artist.ArtistId,
                    FirstName = artist.FirstName,
                    LastName = artist.LastName,
                    Name = artist.Name,
                    Biography = artist.Biography
                });
            }

            // Return the DTO back to the client
            return Ok(artistsDto);
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

            // Map Artist Domain model to ArtistDto
            var artistDto = new ArtistDto
            {
                ArtistId = artist.ArtistId,
                FirstName = artist.FirstName,
                LastName = artist.LastName,
                Name = artist.Name,
                Biography = artist.Biography
            };

            // Return the DTO back to the client 
            return Ok(artistDto);
        }

        // POST: https://localhost:1234/api/artists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddArtistDto addArtistDto)
        {
            // Map DTO to Domain Model
            var artist = new Artist
            {
                FirstName = addArtistDto.FirstName,
                LastName = addArtistDto.LastName,
                Name = addArtistDto.Name,
                Biography = addArtistDto.Biography
            };

            // Use Domain Model to create Artist
            artist = await artistRepository.CreateAsync(artist);

            // Map Domain model back to DTO
            var artistDto = new ArtistDto
            {
                ArtistId = artist.ArtistId,
                FirstName = artist.FirstName,
                LastName = artist.LastName,
                Name = artist.Name,
                Biography = artist.Biography
            };

            return CreatedAtAction(nameof(GetById), new { id = artistDto.ArtistId }, artistDto);
        }

        // PUT: https://localhost:1234/api/artists/114
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArtistDto updateArtistDto)
        {
            // Map DTO to Domain Model
            var artist = new Artist
            {
                FirstName = updateArtistDto.FirstName,
                LastName = updateArtistDto.LastName,
                Name = updateArtistDto.Name,
                Biography = updateArtistDto.Biography
            };

            artist = await artistRepository.UpdateAsync(id, artist);

            if (artist == null)
            {
                return NotFound($"Artist with Id: {id} wasn't found!");
            }

            // Convert Domain Model to DTO
            var artistDto = new ArtistDto
            {
                ArtistId = artist.ArtistId,
                FirstName = artist.FirstName,
                LastName = artist.LastName,
                Name = artist.Name,
                Biography = artist.Biography
            };

            return Ok(artistDto);
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

            // Map the Domain Model to DTO
            var artistDto = new ArtistDto
            {
                ArtistId = artist.ArtistId,
                FirstName = artist.FirstName,
                LastName = artist.LastName,
                Name = artist.Name,
                Biography = artist.Biography
            };

            return Ok(artistDto);
        }
    }
}
