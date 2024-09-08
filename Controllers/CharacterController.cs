
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
        private readonly ICharacterService _characterService;


        public CharacterController(ICharacterService characterService)
        {   
            _characterService = characterService;
            
        }
        // IActionResult this enables us to send specific HTTP status code
        // back to the client together with the actual data that was requested

        //One way to add Route Method
        [HttpGet("GetAll")]

        //The Other way:
        // [HTTPGet]
        // [Route("GetAll")]
        
        public ActionResult<List<Character>> Get(){
            // return BadRequest : 400
            // return NotFound: 404
            return Ok(_characterService.GetAllCharacter());

        }
        // Web Api does not kow which method to use
        // because we have 2 get methods now
        // => add Routing Attribute
        [HttpGet("{id}")]
        //ID is sent through the URL
        public ActionResult<Character> GetSingle(int id){
            // return BadRequest : 400
            // return NotFound: 404
            return Ok(_characterService.GetCharacterById(id));

        }

        // return data type: List<Character>
        // we want to return all our characters so that we can
        // see the change of our characters
        [HttpPost]
        //JSON object will be sent via the body of the request
        public ActionResult <List<Character>> AddCharacter (Character newCharacter){

           
            return Ok(_characterService.AddCharacter(newCharacter));

        }
        
    }
}