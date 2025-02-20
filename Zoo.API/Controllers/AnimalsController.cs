using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Entities;
using Zoo.Core.Interfaces;

namespace Zoo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnimals()
        {
             var response = await _animalService.GetAllAnimals();
            if (!response.Success)
            {
                return NoContent();
            }
            else
            {
                return Ok(response.Data);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _animalService.GetById(id);
            if (!response.Success)
            {
                return NotFound();
            }
            else
            {
                return Ok(response.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAnimal([FromBody] Animal animal)
        {
            var response = await _animalService.AddNewAnimal(animal);
            if (!response.Success)
            {
                return BadRequest();
            } else
            {
                return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
            }
        }

        [HttpPut("{id}/feed")]
        public async Task<IActionResult> FeedAnimal(int id,byte food)
        {
            var IsExisting = await _animalService.GetById(id);
            if (!IsExisting.Success)
            {
                return NotFound();
            }

            var response = await _animalService.FeedAnimal(id, food);
            if (!response.Success)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id) 
        {
            var response = await _animalService.DeleteAnimal(id);
            if (!response.Success)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
    
}
