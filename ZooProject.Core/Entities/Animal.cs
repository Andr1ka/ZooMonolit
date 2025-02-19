using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.Core.Entities
{
    public class Animal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TypeOfAnimal { get; set; }

        public byte Energy { get; set; }
    }
}

