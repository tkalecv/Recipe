using System;
using System.Collections.Generic;
using System.Text;
using Recipe.Models.Common;

namespace Recipe.Models
{
    public class MeasuringUnit : IMeasuringUnit
    {
        public int MeasuringUnitID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
