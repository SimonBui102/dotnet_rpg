using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        


        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper=mapper;
            _context=context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>>  AddCharacter(AddCharacterDto newCharacter){

            var serviceReponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            
            serviceReponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

            return serviceReponse;
        }



        public async Task<ServiceResponse<List<GetCharacterDto>>>  GetAllCharacter(int userId){
            var serviceReponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User!.Id == userId).ToListAsync();
            serviceReponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceReponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>>  GetCharacterById(int id){

            var serviceReponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceReponse.Data=_mapper.Map<GetCharacterDto> (dbCharacter);

            return serviceReponse;       
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (UpdateCharacterDto updatedCharacter){
            var serviceReponse = new ServiceResponse<GetCharacterDto>();
            try{

            
            var character = 
               await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            if(character is null){

                throw new Exception($"Character with id '{updatedCharacter.Id}' not found. ");

            }

            character.Name= updatedCharacter.Name;
            character.HitPoints= updatedCharacter.HitPoints;
            character.Strength= updatedCharacter.Strength;
            character.Defense= updatedCharacter.Defense;
            character.Intenlligence= updatedCharacter.Intenlligence;
            character.Class= updatedCharacter.Class;

            await _context.SaveChangesAsync();

            serviceReponse.Data= _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex){

                serviceReponse.Success= false;
                serviceReponse.Message = ex.Message;

            }

            return serviceReponse;


        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceReponse = new ServiceResponse<List<GetCharacterDto>>();
            try{

            
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if(character is null){

                throw new Exception($"Character with id '{id}' not found. ");

            }

            _context.Characters.Remove(character);

            await _context.SaveChangesAsync();


            serviceReponse.Data= await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch(Exception ex){

                serviceReponse.Success= false;
                serviceReponse.Message = ex.Message;

            }

            return serviceReponse;

        }
        
    }
}