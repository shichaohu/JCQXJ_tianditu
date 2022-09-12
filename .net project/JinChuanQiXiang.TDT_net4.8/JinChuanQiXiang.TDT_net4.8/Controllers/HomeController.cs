using Catfood.Shapefile;
using CsvHelper;
using JinChuanQiXiang.TDT.Common;
using JinChuanQiXiang.TDT.Common.Entity;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using NetTopologySuite.IO.Streams;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace JinChuanQiXiang.TDT_net4._8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCoordinatesData()
        {
            //FeatureCollection collection1 = ReadShapeFile();
            ShpToFeatureCollection1(out FeatureCollection collection);
            ConvertFeatureCollection(collection, out StringBuilder stringBuilder);

            string str = stringBuilder.ToString();
            var fc = JsonConvert.DeserializeObject<FeatureCollectionDTO>(str);
            var positions = new List<List<decimal[]>>();
            foreach (var feature in fc.features)
            {
                var pos = new List<decimal[]>();
                foreach (var coordinates in feature.geometry.coordinates)
                {
                    pos.Add(coordinates);
                }
                //pos = pos.OrderBy(x => x[0]).ThenBy(x => x[1]).ToList();
                positions.Add(pos);
            }

            return Json(positions);
        }
        public static FeatureCollection ReadShapeFile()
        {
            string pathName = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/Map/jinchuan.shp");
            FeatureCollection featureCollection = new FeatureCollection();
            ShapefileDataReader dataReader = new ShapefileDataReader(pathName, GeometryFactory.Default);

            while (dataReader.Read())
            {
                Feature feature = new Feature { Geometry = dataReader.Geometry };
                int length = dataReader.DbaseHeader.NumFields;
                feature.Attributes = new AttributesTable();
                for (var i = 0; i < length; i++)
                {
                    var keys = dataReader.DbaseHeader.Fields[i].Name;
                    var value = dataReader.GetValue(i).ToString();
                    feature.Attributes.Add(keys, value);
                }
                featureCollection.Add(feature);
            }
            return featureCollection;
        }

        public ActionResult LoadCSVFile()
        {
            //读CSV文件
            string path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/Stations/jinchuan_stations.csv");
            using (var reader = new StreamReader(path, Encoding.Default))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<StationDtoMap>();  //配置匹配类
                var stations = csv.GetRecords<StationDto>().ToList();

                return Json(stations);
            }
        }
        public bool ShpToFeatureCollection1(out FeatureCollection featureCollection)
        {
            string filename = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/Map/jinchuan");
            featureCollection = new FeatureCollection();
            try
            {
                if (!System.IO.File.Exists(filename + ".dbf"))
                {
                    return false;
                }
                var streamreader = new ShapefileStreamProviderRegistry(filename);
                var dataReader = new ShapefileDataReader(streamreader, new GeometryFactory(new PrecisionModel()));

                while (dataReader.Read())
                {
                    var feature = new NetTopologySuite.Features.Feature { Geometry = dataReader.Geometry };
                    int length = dataReader.DbaseHeader.NumFields;
                    string[] keys = new string[length];
                    for (int i = 0; i < length; i++)
                        keys[i] = dataReader.DbaseHeader.Fields[i].Name;

                    feature.Attributes = new AttributesTable();
                    for (int i = 0; i < length; i++)
                    {
                        object val = dataReader.GetValue(i + 1);
                        feature.Attributes.Add(keys[i], val);
                    }

                    featureCollection.Add(feature);
                }
                dataReader.Close();
                dataReader.Dispose();

                if (featureCollection.Count == 0)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        private bool ConvertFeatureCollection(FeatureCollection value, out StringBuilder stringBuilder)
        {
            FeatureCollectionConverter target = new FeatureCollectionConverter();
            stringBuilder = new StringBuilder();
            JsonTextWriter writer = new JsonTextWriter(new StringWriter(stringBuilder));
            JsonSerializer serializer = NetTopologySuite.IO.GeoJsonSerializer.Create(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore },
                GeometryFactory.Default);
            target.WriteJson(writer, value, serializer);
            writer.Flush();
            writer.Close();

            if (stringBuilder.Length == 0)
            {
                return false;
            }
            return true;
        }
        public bool ReadGeojson(string geojson, string fileName)
        {
            bool bResult = true;
            try
            {
                do
                {
                    NetTopologySuite.IO.GeoJsonReader reader = new NetTopologySuite.IO.GeoJsonReader();
                    FeatureCollection result = reader.Read<FeatureCollection>(geojson);

                    if (result == null)
                    {
                        bResult = false;
                        break;
                    }

                    //ShapefileWriter.WriteFeatures(fileName, ((Collection<IFeature>)result).AsEnumerable());
                }
                while (false);
            }
            catch (Exception ex)
            {
                bResult = false;
            }
            return bResult;
        }
    }
}