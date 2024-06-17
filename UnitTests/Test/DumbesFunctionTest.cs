using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test
{
    public static class DumbesFunctionTest
    {
        //Naming convention: class name + method name + condition
        public  static void DumbesFunction_ReturnsPokemon_ReturnsBulbasaur()
        {
            try
            {
                int num = 0;
                DumbesFunction dumbesFunction = new DumbesFunction();
                string result = dumbesFunction.ReturnsPokemon(num);
                if(result == "Bulbasaur")
                {
                    Console.WriteLine("Test Passed");
                }
                else
                {
                    Console.WriteLine("Test Failed");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //DumbesFunction dumbesFunction = new DumbesFunction();
            //string result = dumbesFunction.ReturnsPokemon(0);
            //Assert.AreEqual("Bulbasaur", result);
        }
    }
}
