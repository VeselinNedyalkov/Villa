using Microsoft.AspNetCore.Mvc;
using TrainingAPI.Data;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villasList);
        }

        [HttpGet("id")] //expect parameter id
        [ProducesResponseType(200)] //document possible code response
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
    }
}
