using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class DumbesFunction
    {
        public string ReturnsPokemon(int num) {
            if(num == 0)
            {
                return "Bulbasaur";
            }else if(num == 1)
            {
                return "Ivysaur";
            }
            return "Charmander";
        }
    }
}
