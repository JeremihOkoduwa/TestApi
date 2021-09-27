using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.Model.BaseModel;

namespace Test.Core
{
    public class Country : BaseModels
    {
        public string Rank { get; set; }
        public string Nation { get; set; }
        public string Popuation { get; set; }
        public string Date { get; set; }
        public string PercentageOfWorldPopulation { get; set; }
    }
}
