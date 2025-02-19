using Zoo.Core.Interfaces;

namespace Zoo.API.Controllers
{
    public class AnimalsController
    {
        private readonly IAnimalService _animalService;

        AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }   

    }
}
