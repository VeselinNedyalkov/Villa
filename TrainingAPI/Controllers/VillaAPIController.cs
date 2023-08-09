using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingAPI.Data;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public ILogger<VillaAPIController> logger { get; }
        private readonly AplicationDbContext db;

        public VillaAPIController(ILogger<VillaAPIController> _logger,
            AplicationDbContext _db)
        {
            db = _db;
            logger = _logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            logger.LogInformation("Getting all villas");
            return Ok(db.Villas.ToArray());
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
            var villa = db.Villas.FirstOrDefault(x => x.Id == id);

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

            if(db.Villas.FirstOrDefault(x => x.Name == villa.Name) != null)
            {
                ModelState.AddModelError("CustomNameError", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest(villa);
            }

            Villa model = new Villa()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };

            db.Villas.Add(model);
            db.SaveChanges();
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

            var villa = db.Villas.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            db.Villas.Remove(villa);
            db.SaveChanges();

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

            Villa model = new Villa()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };

            db.Villas.Update(model);
            db.SaveChanges();

            return NoContent();
        }


        [HttpPatch("id", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id != 0)
            {
                return BadRequest();
            }
            var villa = db.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            VillaDTO vila = new ()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };

            patchDTO.ApplyTo(vila, ModelState);

            Villa model = new Villa()
            {
                Amenity = vila.Amenity,
                Details = vila.Details,
                Id = vila.Id,
                ImageUrl = vila.ImageUrl,
                Name = vila.Name,
                Occupancy = vila.Occupancy,
                Rate = vila.Rate,
                Sqft = vila.Sqft,
            };


            db.Villas.Update(model);
            db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
