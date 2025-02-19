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
        public IActionResult GetAllAnimals()
        {
            var response = _animalService.GetAllAnimals();
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
        public IActionResult GetById(int id)
        {
            var response = _animalService.GetById(id);
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
        public IActionResult AddNewAnimal([FromBody] Animal animal)
        {
            var response = _animalService.AddNewAnimal(animal);
            if (!response.Success)
            {
                return BadRequest();
            } else
            {
                return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
            }
        }

        [HttpPut("{id}/feed")]
        public IActionResult FeedAnimal(int id,byte food)
        {
            if (!_animalService.GetById(id).Success)
            {
                return NotFound();
            }

            var response = _animalService.FeedAnimal(id, food);
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
        public IActionResult DeleteAnimal(int id) 
        {
            var response = _animalService.DeleteAnimal(id);
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
