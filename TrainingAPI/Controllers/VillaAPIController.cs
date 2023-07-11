using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TrainingAPI.Data;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public ILogger<VillaAPIController> logger { get; }

        public VillaAPIController(ILogger<VillaAPIController> _logger)
        {
            logger = _logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            logger.LogInformation("Getting all villas");
            return Ok(VillaStore.villasList);
        }

        [HttpGet("id" , Name = "GetVilla")] //expect parameter id
        [ProducesResponseType(StatusCodes.Status200OK)] //document possible code response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa =  Ok(VillaStore.villasList.FirstOrDefault(x => x.Id == id));

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villa)
        {
            //we need this if we don`t use  [ApiController] for the class
            //if(ModelState.IsValid) { }

            if(VillaStore.villasList.FirstOrDefault(x => x.Name == villa.Name) != null)
            {
                ModelState.AddModelError("CustomNameError", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest(villa);
            }
            

            villa.Id = VillaStore.villasList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villasList.Add(villa);

            //create a route to the villa [HttpGet(Name = "GetVilla")] call the name send the ID and the VllaDTO

            return CreatedAtRoute("GetVilla" ,new {id = villa.Id} ,villa);
        }

        [HttpDelete("id" , Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villasList.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.villasList.Remove(villa);

            return NoContent();
        }


        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO) 
        { 
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villasList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;


            return NoContent();
        }


        [HttpPatch("id", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatepaartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id != 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villasList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
