using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public ILogger<VillaAPIController> logger { get; }
        private readonly IRepository dbVilla;
        private readonly IMapper mapper;

        public VillaAPIController(IRepository _dbVilla, IMapper _mapper)
        {
            dbVilla = _dbVilla;
            mapper = _mapper;
            this.response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villas = await dbVilla.GetAllAsync();
                response.Result = mapper.Map<List<VillaDTO>>(villas);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsUsccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }

        [HttpGet("id", Name = "GetVilla")] //expect parameter id
        [ProducesResponseType(StatusCodes.Status200OK)] //document possible code response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var villa = await dbVilla.GetVillaAsync(u => u.Id == id);

                if (villa == null)
                {
                    return NotFound();
                }

                response.Result = mapper.Map<VillaDTO>(villa);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsUsccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }

        [HttpPost(Name = "Create new villa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDto)
        {
            //we need this if we don`t use  [ApiController] for the class
            //if(ModelState.IsValid) { }

            try
            {
                if (await dbVilla.GetVillaAsync(x => x.Name.ToLower() == createDto.Name) != null)
                {
                    ModelState.AddModelError("CustomNameError", "Villa already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
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
                response.IsUsccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }

        [HttpDelete("id", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response>> DeleteVilla(int id)
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

            return NoContent();
        }


        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            var villa = await dbVilla.GetVillaAsync(x => x.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            Villa model = mapper.Map<Villa>(updateDTO);

            await dbVilla.UpdateAsync(model);

            return NoContent();
        }


        [HttpPatch("id", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id != 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var villa = await dbVilla.GetVillaAsync(x => x.Id == id);

            if (villa == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            VillaUpdateDTO vila = mapper.Map<VillaUpdateDTO>(patchDTO);

            patchDTO.ApplyTo(vila, ModelState);

            Villa model = mapper.Map<Villa>(vila);


            await dbVilla.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            return NoContent();
        }
    }
}
