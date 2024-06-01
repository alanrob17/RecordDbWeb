using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordDb.API.CustomActionFilters;
using RecordDb.API.Data;
using RecordDb.API.Models.Domain;
using RecordDb.API.Models.DTO;
using RecordDb.API.SQLRepository;
using System.Runtime.InteropServices;

namespace RecordDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordRepository recordRepository;
        private readonly IMapper mapper;

        public RecordsController(IRecordRepository recordRepository, IMapper mapper)
        {
            this.recordRepository = recordRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:1234/api/records?filterOn=field&filterQuery=recorded&sortOn=name&isAscending=true&pageNumber=1,pageSize=15
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            // GET data from the database - Domain Model
            var records = await recordRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Return the DTO back to the client
            return Ok(mapper.Map<List<RecordQueryDto>>(records));
        }

        // GET: https://localhost:1234/api/artists/114
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // GET Artist Domain mode from database
            var record = await recordRepository.GetByIdAsync(id);

            if (record == null)
            {
                return NotFound($"An Record with Id: {id} wasn't found!");
            }

            // Return the DTO back to the client 
            return Ok(mapper.Map<RecordDto>(record));
        }

        // POST: https://localhost:1234/api/records
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRecordDto addRecordDto)
        {
            // Map DTO to Domain Model
            var record = mapper.Map<Record>(addRecordDto);

            // Use Domain Model to create Artist
            record = await recordRepository.CreateAsync(record);

            // Map Domain model back to DTO
            var recordDto = mapper.Map<RecordDto>(record);

            return CreatedAtAction(nameof(GetById), new { id = recordDto.ArtistId }, recordDto);
        }

        // PUT: https://localhost:1234/api/artists/114
        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRecordDto updateRecordDto)
        {
            // Map DTO to Domain Model
            var record = mapper.Map<Record>(updateRecordDto);

            record = await recordRepository.UpdateAsync(id, record);

            if (record == null)
            {
                return NotFound($"Record with Id: {id} wasn't found!");
            }

            // Return the DTO back to the client 
            return Ok(mapper.Map<RecordDto>(record));
        }

        // DELETE: https://localhost:1234/api/artists/114
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var record = await recordRepository.DeleteAsync(id);

            if (record == null)
            {
                return NotFound($"Record with Id: {id} not found!");
            }

            // Return the DTO back to the client 
            return Ok(mapper.Map<RecordDto>(record));
        }
    }
}
