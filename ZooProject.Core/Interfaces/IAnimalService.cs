using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Entities;
using Zoo.Core.Responses;

namespace Zoo.Core.Interfaces
{
    public interface IAnimalService
    {
        public AnimalResult<IEnumerable<Animal>> GetAllAnimals();
        public AnimalResult<Animal> GetById(int id);
        public AnimalResult<Animal> AddNewAnimal(Animal animal);
        public AnimalResult<Animal> FeedAnimal(int id, byte food);
        public AnimalResult<Animal> DeleteAnimal(int id);
    }
}
