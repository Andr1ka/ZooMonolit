using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Entities;
using Zoo.Core.Interfaces;
using Zoo.Core.Responses;

namespace Zoo.Core.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }


        public AnimalResult<Animal> AddNewAnimal(Animal animal)
        {
            if (animal.Name is null || animal.TypeOfAnimal is null) new AnimalResult<Animal>
            {
                Success = false
            };

            animal.Energy = 100;
            var response = _animalRepository.AddNewAnimal(animal);

            return new AnimalResult<Animal>
            {
                Success = response.Result.Success
            };
        }

        public AnimalResult<Animal> DeleteAnimal(int id)
        {
            var response = _animalRepository.DeleteAnimal(id);

            return new AnimalResult<Animal>
            {
                Success = response.Result.Success,
            };
        }

        public AnimalResult<Animal> FeedAnimal(int id, byte food)
        {
            
            var response = _animalRepository.FeedAnimal(id,food);
            return new AnimalResult<Animal> 
            {
                Success = response.Result.Success
            };
        }

        public AnimalResult<IEnumerable<Animal>> GetAllAnimals()
        {
            var response = _animalRepository.GetAllAnimals();

            return new AnimalResult<IEnumerable<Animal>>
            {
                Success = response.Result.Success,
                Data = response.Result.Data
            };
        }

        public AnimalResult<Animal> GetById(int id)
        {
            var response =_animalRepository.GetById(id);
            return new AnimalResult<Animal>
            {
                Success = response.Result.Success,
                Data = response.Result.Data
            };


        }
    }
}
