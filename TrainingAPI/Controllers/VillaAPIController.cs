using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingAPI.Data;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public ILogger<VillaAPIController> logger { get; }
        private readonly IRepository dbVilla;
        private readonly IMapper mapper;

        public VillaAPIController(ILogger<VillaAPIController> _logger,
            IRepository _dbVilla, IMapper _mapper)
        {
            dbVilla = _dbVilla;
            mapper = _mapper;
            logger = _logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villas = await dbVilla.GetAllAsync();
            return  Ok(mapper.Map<List<VillaDTO>>(villas));
        }

        [HttpGet("id" , Name = "GetVilla")] //expect parameter id
        [ProducesResponseType(StatusCodes.Status200OK)] //document possible code response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await dbVilla.GetVillaAsync(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO createDto)
        {
            //we need this if we don`t use  [ApiController] for the class
            //if(ModelState.IsValid) { }

            if(await dbVilla.GetVillaAsync(x => x.Name.ToLower() == createDto.Name) != null)
            {
                ModelState.AddModelError("CustomNameError", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }

           Villa model = mapper.Map<Villa>(createDto);

            await dbVilla.CreateAsync(model);
            //create a route to the villa [HttpGet(Name = "GetVilla")] call the name send the ID and the VllaDTO

            return CreatedAtRoute("GetVilla" ,new {id = model.Id} ,createDto);
        }

        [HttpDelete("id" , Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await dbVilla.GetVillaAsync(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            dbVilla.RemoveAsync(villa);

            return NoContent();
        }


        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO updateDTO) 
        { 
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }

            var villa = await dbVilla.GetVillaAsync(x => x.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }

            Villa model = mapper.Map<Villa>(updateDTO);

            dbVilla.UpdateAsync(model);

            return NoContent();
        }


        [HttpPatch("id", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id != 0)
            {
                return BadRequest();
            }
            var villa = await dbVilla.GetVillaAsync(x => x.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            VillaUpdateDTO vila = mapper.Map<VillaUpdateDTO>(patchDTO);

            patchDTO.ApplyTo(vila, ModelState);

            Villa model = mapper.Map<Villa>(vila);


            dbVilla.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
