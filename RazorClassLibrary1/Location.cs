using System;
using System.Collections.Generic;
using System.Text;

namespace RazorClassLibrary1
{
   public class Location
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Accuracy { get; set; }

        public override string ToString()
        {
            return $"Location: ({Latitude}, {Longitude}) accuracy {Accuracy}";
        }
    }
}
