using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinChuanQiXiang.TDT.Common
{
    public class FeatureCollectionDTO
    {
        public string type { get; set; }
        public List<FeaturesBody >features { get; set; }
    }
    public class FeaturesBody
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
    }
    public class Geometry
    {
        public string type { get; set; }
        public List<decimal[]> coordinates { get; set; }

    }
}
