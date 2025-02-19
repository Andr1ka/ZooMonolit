using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Entities;
using Zoo.Core.Responses;

namespace Zoo.Core.Interfaces
{
    public interface IAnimalRepository
    {
        public Task<AnimalResult<IEnumerable<Animal>>> GetAllAnimals();
        public Task<AnimalResult<Animal>> GetById(int id);
        public Task<AnimalResult<Animal>> AddNewAnimal(Animal animal);
        public Task<AnimalResult<Animal>> FeedAnimal(int id, byte food);
        public Task<AnimalResult<Animal>> DeleteAnimal(int id);
    }
}
