using Catfood.Shapefile;
using JinChuanQiXiang.TDT.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JinChuanQiXiang.TDT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment WebHostEnvironment)
        {
            _logger = logger;
            webHostEnvironment = WebHostEnvironment;
        }

        public IActionResult Index()
        {
            GetCoordinatesFromShapeFile();
            return View();
        }

        public ActionResult GetCoordinatesFromShapeFile()
        {
            Shapefile shapefile;
            var shapeCoordinates = new Dictionary<string, string>();
            try
            {
                string shapeFilePath = Path.Combine(webHostEnvironment.WebRootPath, "Content/Map/jinchuan.shp");
                using (shapefile = new Shapefile(shapeFilePath))//, Shapefile.ConnectionStringTemplateJet))
                {

                    foreach (Shape shape in shapefile)
                    {
                        ShapePolygon? poligono = shape as ShapePolygon;
                        if (poligono != null)
                        {
                            foreach (PointD[] part in poligono.Parts)
                            {
                                int i = 0;
                                foreach (PointD point in part)
                                {
                                    shapeCoordinates.Add(i.ToString(), point.X.ToString() + ";" + point.Y.ToString());
                                    i++;
                                }
                            }
                        }
                    }
                }
                

                return Json(shapeCoordinates);
            }
            catch (FileNotFoundException e)
            {
                var negativeResponse = new Dictionary<string, string>();
                negativeResponse.Add("0", "Error");

                return Json(negativeResponse);
            }
        }




















        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}