
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();
        // IActionResult this enables us to send specific HTTP status code
        // back to the client together with the actual data that was requested


        [HttpGet]
        public ActionResult<Character> Get(){
            // return BadRequest : 400
            // return NotFound: 404
            return Ok(knight);

        }
        
    }
}