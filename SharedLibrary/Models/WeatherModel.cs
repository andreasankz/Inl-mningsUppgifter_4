using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class WeatherModel
    {
        public class Main
        {
            public double Temp { get; set; }
            public double Humidity { get; set; }
        }


        public class Root
        {
            public string Name { get; set; }
            public Main Main { get; set; }  
        }

    }
}
