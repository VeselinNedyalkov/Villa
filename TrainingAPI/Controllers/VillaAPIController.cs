using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TrainingAPI.Models;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        protected APIResponse response;
        private readonly IVillaRepository dbVilla;
        private readonly IMapper mapper;

        public VillaAPIController(IVillaRepository _dbVilla, IMapper _mapper)
        {
            dbVilla = _dbVilla;
            mapper = _mapper;
            response = new APIResponse();
        }


        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        //cache the data for 30 sec
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetAllVillas([FromQuery(Name = "FilterOccupancy")] int? occupancy,
            [FromQuery] string? search,int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Villa> villas;

                if (occupancy > 0)
                {
                    villas = await dbVilla.GetAllAsync(v => v.Occupancy == occupancy, pageSize:pageSize,
                        pageNumber:pageNumber);
                }
                else
                {
                    villas = await dbVilla.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    villas = villas.Where(u => u.Name.ToLower().Contains(search));
                }
                Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize};

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                response.Result = mapper.Map<List<VillaDTO>>(villas);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }

        [HttpGet("id", Name = "GetVilla")] //expect parameter id
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)] //document possible code response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Id is 0");
                    return BadRequest(response);
                }
                var villa = await dbVilla.GetVillaAsync(u => u.Id == id);

                if (villa == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                }

                response.Result = mapper.Map<VillaDTO>(villa);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }

        [HttpPost(Name = "Create new villa")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDto)
        {

            try
            {
                if (await dbVilla.GetVillaAsync(x => x.Name.ToLower() == createDto.Name) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Data is missing");
                    return BadRequest(response);
                }

                Villa villa = mapper.Map<Villa>(createDto);

                await dbVilla.CreateAsync(villa);
                //create a route to the villa [HttpGet(Name = "GetVilla")] call the name send the ID and the VllaDTO
                response.Result = mapper.Map<VillaDTO>(villa);
                response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = villa.Id }, response);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }

        [HttpDelete("id", Name = "DeleteVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }

                var villa = await dbVilla.GetVillaAsync(x => x.Id == id);

                if (villa == null)
                {
                    return NotFound();
                }

                await dbVilla.RemoveAsync(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;

                return Ok(response);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return response;
        }

        [Authorize(Roles = "admin")]
        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Data is missing");
                    return BadRequest(response);
                }

                Villa model = mapper.Map<Villa>(updateDTO);

                await dbVilla.UpdateAsync(model);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages =  new List<string>() { ex.ToString() };
            }

            return response;
        }


        [HttpPatch("id", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Data is missing");
                return BadRequest(response);
            }
            var villa = await dbVilla.GetVillaAsync(u => u.Id == id, tracked: false);

            VillaUpdateDTO villaDTO = mapper.Map<VillaUpdateDTO>(villa);


            if (villa == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Data is missing");
                return BadRequest(response);
            }
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = mapper.Map<Villa>(villaDTO);

            await dbVilla.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
