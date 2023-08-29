using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrainingAPI.Models;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Controllers
{
    [Route("api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse response;
        private IRepository villaDb;
        private readonly INumberRepository dbVillaNumber;
        private readonly IMapper mapper;

        public VillaNumberAPIController(INumberRepository _dbVillaNumber,
            IMapper _mapper, IRepository _vilaDb)
        {
            dbVillaNumber = _dbVillaNumber;
            villaDb = _vilaDb;
            mapper = _mapper;
            this.response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villas = await dbVillaNumber.GetAllAsync();
                response.Result = mapper.Map<List<VillaNumberDTO>>(villas);
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

        [HttpGet("id", Name = "GetVillaNumber")] //expect parameter id
        [ProducesResponseType(StatusCodes.Status200OK)] //document possible code response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers(int id)
        {
            try
            {
                if (id == 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var villa = await dbVillaNumber.GetVillaAsync(u => u.VillaNo == id);

                if (villa == null)
                {
                    return NotFound();
                }

                response.Result = mapper.Map<VillaNumberDTO>(villa);
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

        [HttpPost(Name = "Create new villa number")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaCreateNumberDTO createDto)
        {
            //we need this if we don`t use  [ApiController] for the class
            //if(ModelState.IsValid) { }

            try
            {
                if (await dbVillaNumber.GetVillaAsync(x => x.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("CustomNameError", "Villa Number already Exists!");
                  
                    return BadRequest(ModelState);
                }

                if(await villaDb.GetVillaAsync(x => x.Id == createDto.VillaID) == null)
                {
                    ModelState.AddModelError("CustomNameError", "Villa Id is invalid!");

                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                VillaNumber villa = mapper.Map<VillaNumber>(createDto);

                await dbVillaNumber.CreateAsync(villa);
                //create a route to the villa [HttpGet(Name = "GetVilla")] call the name send the ID and the VllaDTO
                response.Result = mapper.Map<VillaNumberDTO>(villa);
                response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = villa.VillaNo }, response);

            }
            catch (Exception ex)
            {
                response.IsUsccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return response;
        }

        [HttpDelete("id", Name = "Delete Villa number")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response>> DeleteVillaNumber(int id)
        {
            if (id == 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            var villa = await dbVillaNumber.GetVillaAsync(x => x.VillaNo == id);

            if (villa == null)
            {
                return NotFound();
            }

            await dbVillaNumber.RemoveAsync(villa);

            return NoContent();
        }


        [HttpPut("id", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVillaNumber(int id, [FromBody] VillaUpdateNumberDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.VillaNo)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            if (await villaDb.GetVillaAsync(x => x.Id == updateDTO.VillaID) == null)
            {
                ModelState.AddModelError("CustomNameError", "Villa Id is invalid!");

                return BadRequest(ModelState);
            }

            var villa = await dbVillaNumber.GetVillaAsync(x => x.VillaNo == id);

            if (villa == null)
            {
                return BadRequest();
            }

            VillaNumber model = mapper.Map<VillaNumber>(updateDTO);

            await dbVillaNumber.UpdateNumberAsync(model);

            return NoContent();
        }
       
    }
}
