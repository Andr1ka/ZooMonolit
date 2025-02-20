using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Entities;
using Zoo.Core.Interfaces;
using Zoo.Core.Responses;
using Zoo.Infrastructure.Data;

namespace Zoo.Infrastructure.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ZooDbContext _dbContext;
        public AnimalRepository(ZooDbContext animalRepository)
        {
            _dbContext = animalRepository;
        }

        public async Task<AnimalResult<Animal>> AddNewAnimal(Animal animal)
        {
            _dbContext.Animals.Add(animal);
            await _dbContext.SaveChangesAsync();

            return new AnimalResult<Animal>
            {
                Success = true,
                Data = animal
            };
        }

        public async Task<AnimalResult<Animal>> DeleteAnimal(int id)
        {
            var animalResult = await GetById(id);
            if (!animalResult.Success)
                return new AnimalResult<Animal>
                {
                    Success = false,
                    ErrorMessage = $"Animal with ID {id} not found."
                };

            var animal = animalResult.Data;
            _dbContext.Animals.Remove(animal);
            await _dbContext.SaveChangesAsync();

            return new AnimalResult<Animal>
            {
                Success = true,
                Data = animal
            };
        }

        public async Task<AnimalResult<Animal>> FeedAnimal(int id, byte food)
        {
            var animalResult = await GetById(id);
            if (!animalResult.Success)
                return new AnimalResult<Animal>
                {
                    Success = false,
                    ErrorMessage = $"Animal with ID {id} not found."
                };

            var animal = animalResult.Data;
            animal.Energy = (byte)Math.Min(animal.Energy + food, 100);
            _dbContext.Animals.Update(animal);
            await _dbContext.SaveChangesAsync();
            return new AnimalResult<Animal>
            {
                Success = true,
                Data = animal
            };
        }

        public async Task<AnimalResult<IEnumerable<Animal>>> GetAllAnimals()
        {
            var animalsList = await _dbContext.Animals
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (animalsList.Count > 0)
            {
                return new AnimalResult<IEnumerable<Animal>>
                {
                    Success = true,
                    Data = animalsList
                };
            }

            return new AnimalResult<IEnumerable<Animal>>
            {
                Success = false,
                ErrorMessage = "No animals in zoo"
            };
        }

        public async Task<AnimalResult<Animal>> GetById(int id)
        {
            var animal = await _dbContext.Animals.FindAsync(id);
            if (animal == null)
            {
                return new AnimalResult<Animal>
                {
                    Success = false,
                    ErrorMessage = $"Animal with ID {id} not found."
                };
            }
            return new AnimalResult<Animal>
            {
                Success = true,
                Data = animal
            };
        }
    }
}
