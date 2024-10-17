
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
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
        
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get(){
            // return BadRequest : 400
            // return NotFound: 404

           // int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            return Ok(await _characterService.GetAllCharacter());

        }
        // Web Api does not kow which method to use
        // because we have 2 get methods now
        // => add Routing Attribute
        [HttpGet("{id}")]
        //ID is sent through the URL
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id){
            // return BadRequest : 400
            // return NotFound: 404
            return Ok( await _characterService.GetCharacterById(id));

        }

        // return data type: List<Character>
        // we want to return all our characters so that we can
        // see the change of our characters
        [HttpPost]
        //JSON object will be sent via the body of the request
        public async Task<ActionResult <ServiceResponse<List<GetCharacterDto>>>> AddCharacter (AddCharacterDto newCharacter){

           
            return Ok(await _characterService.AddCharacter(newCharacter));

        }


        [HttpPut]
        //JSON object will be sent via the body of the request
        public async Task<ActionResult <ServiceResponse<GetCharacterDto>>> UpdateCharacter (UpdateCharacterDto updatedCharacter){
            
            var response= await _characterService.UpdateCharacter(updatedCharacter);

            if(response.Data is null){

                return NotFound(response);
            }
           
            return Ok(response);

        }



        [HttpDelete("{id}")]
        //ID is sent through the URL
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id){

            var response= await _characterService.DeleteCharacter(id);

            if(response.Data is null){

                return NotFound(response);
            }
           
            return Ok(response);

        }
        
    }
}