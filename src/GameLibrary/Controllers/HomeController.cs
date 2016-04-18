using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameLibrary.Controllers
{

    public class Game
        {
            public int appid { get; set; }
            public string name { get; set; }
            public int playtime_forever { get; set; }
            public string img_icon_url { get; set; }
            public string img_logo_url { get; set; }
            public bool has_community_visible_stats { get; set; }
            public int? playtime_2weeks { get; set; }
        }

        public class Response
        {
            public int game_count { get; set; }
            public List<Game> games { get; set; }
        }

        public class RootObject
        {
            public Response response { get; set; }
        }
    public class HomeController : Controller
    {

        public async Task<string> getJSON(string path)
        {
            var client = new HttpClient();

            var task = await client.GetAsync(path);
            var jsonString = await task.Content.ReadAsStringAsync();
            return jsonString;
        }
        public async Task<IActionResult> Index()
        {
            string apikey = "73D1971C481D51A3E934FA1A7D04788E";
            string steamid = "76561198001524134";
            string path = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + apikey + "&steamid=" + steamid + "&include_appinfo=1&format=json";

            List<Game> games= null;

            var jsonString = await getJSON(path);
            games = JsonConvert.DeserializeObject<RootObject>(jsonString).response.games;
            foreach(Game g in games)
            {
                string dbPath = "http://thegamesdb.net/api/GetGame.php?platform=pc&name=" + g.name.Replace(':',' ');
                XDocument gameDB= GetXmlDataFromUrl(dbPath);
                try
                {
                    string gameBoxArt = (from image in gameDB.Descendants("Images") select (string)image.Element("fanart")).First();
                    g.img_icon_url = "http://thegamesdb.net/banners/" + gameBoxArt;
                }
                catch (Exception e) {
                    //try
                    //{

                    //    XDocument gameDB = GetXmlDataFromUrl(dbPath);
                    //}
                    //catch (Exception f) { }
                }
            }
            
            return View(games);
        }
        public static XDocument GetXmlDataFromUrl(string url)
        {


            //load the file from the stream
            var document = XDocument.Load(url);

            

            return document;
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
