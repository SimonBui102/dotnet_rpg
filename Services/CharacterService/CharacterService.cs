using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {Id = 1,Name = "Sam"}


        };

        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper=mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>>  AddCharacter(AddCharacterDto newCharacter){

            var serviceReponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) +1;
            characters.Add(character);
            serviceReponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceReponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>>  GetAllCharacter(){
            var serviceReponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceReponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceReponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>>  GetCharacterById(int id){

            var serviceReponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceReponse.Data=_mapper.Map<GetCharacterDto> (character);

            return serviceReponse;       
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (UpdateCharacterDto updatedCharacter){
            var serviceReponse = new ServiceResponse<GetCharacterDto>();
            try{

            
            var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
            if(character is null){

                throw new Exception($"Character with id '{updatedCharacter.Id}' not found. ");

            }

            character.Name= updatedCharacter.Name;
            character.HitPoints= updatedCharacter.HitPoints;
            character.Strength= updatedCharacter.Strength;
            character.Defense= updatedCharacter.Defense;
            character.Intenlligence= updatedCharacter.Intenlligence;
            character.Class= updatedCharacter.Class;

            serviceReponse.Data= _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex){

                serviceReponse.Success= false;
                serviceReponse.Message = ex.Message;

            }

            return serviceReponse;


        }
        
    }
}