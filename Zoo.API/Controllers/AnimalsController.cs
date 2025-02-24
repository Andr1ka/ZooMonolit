using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Entities;
using Zoo.Core.Interfaces;

namespace Zoo.API.Controllers
{
    /// <summary>
    /// Контроллер для управления животными в зоопарке
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="animalService">Сервис для работы с животными</param>
        AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        /// <summary>
        /// Получение всех животных
        /// </summary>
        /// <returns>Список животных</returns>
        /// <response code="200">Успешное выполнение. Возврат информации о всех существующих животных</response>
        /// <response code="204">Ни одного животного еще не добавлено</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        /// <summary>
        /// Получение информации о животном по его id
        /// </summary>
        /// <param name="id">Идентификатор животного.</param>
        /// <returns>Информация о животном</returns>
        /// <response code="200">Успешное выполнение. Возврат информации о животном</response>
        /// <response code="404">Животное с заданным id не найдено</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Добавление нового животного
        /// </summary>
        /// <param name="animal">Данные нового животного.</param>
        /// <returns>Информация о добавленном животном</returns>
        /// <response code="201">Успешное выполнение. Возврат информации о добавленном животном</response>
        /// <response code="400">Добавление завершилось с ошибкой</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Кормление животного с указанным id
        /// </summary>
        /// <param name = "id" > Идентификатор животного.</param>
        /// <param name="food">Количество пищи для кормления.</param>
        /// <returns>Результат операции кормления.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Животное с заданным id не найдено</response>
        /// <response code="400">Кормление завершилось с ошибкой</response>
        [HttpPut("{id}/feed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Удаление животного c указанным id
        /// </summary>
        /// <param name="id">Идентификатор животного.</param>
        /// <returns>Результат операции удаления.</returns>
        /// <response code="204">Успешное выполнение</response>
        /// <response code="404">Животное с заданным id не найдено</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
