﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Entities;
using Zoo.Core.Interfaces;
using Zoo.Core.Responses;
using Zoo.Infrastructure.Configuration;


namespace Zoo.Core.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly AnimalSettings _animalSettings;

        public AnimalService(IAnimalRepository animalRepository, AnimalSettings animalSettings)
        {
            _animalRepository = animalRepository;
            _animalSettings = animalSettings;
        }


        public async Task<AnimalResult<Animal>> AddNewAnimal(Animal animal)
        {
            if (animal.Name is null || animal.TypeOfAnimal is null) new AnimalResult<Animal>
            {
                Success = false
            };

            animal.Energy = _animalSettings.DefaultEnergy;
            var response = await _animalRepository.AddNewAnimal(animal);

            return new AnimalResult<Animal>
            {
                Success = response.Success
            };
        }

        public async Task<AnimalResult<Animal>> DeleteAnimal(int id)
        {
            var response = await _animalRepository.DeleteAnimal(id);

            return new AnimalResult<Animal>
            {
                Success = response.Success,
            };
        }

        public async Task<AnimalResult<Animal>> FeedAnimal(int id, byte food)
        {
            
            var response = await _animalRepository.FeedAnimal(id,food);
            return new AnimalResult<Animal> 
            {
                Success = response.Success
            };
        }

        public async Task<AnimalResult<IEnumerable<Animal>>> GetAllAnimals()
        {
            var response = await _animalRepository.GetAllAnimals();

            return new AnimalResult<IEnumerable<Animal>>
            {
                Success = response.Success,
                Data = response.Data
            };
        }

        public async Task<AnimalResult<Animal>> GetById(int id)
        {
            var response = await _animalRepository.GetById(id);

            return new AnimalResult<Animal>
            {
                Success = response.Success,
                Data = response.Data
            };


        }
    }
}
