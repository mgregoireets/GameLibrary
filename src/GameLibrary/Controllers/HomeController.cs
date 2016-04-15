using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

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


        public  async Task<IActionResult> Index()
        {
            string apikey = "73D1971C481D51A3E934FA1A7D04788E";
            string steamid = "76561198001524134";
            string path = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + apikey + "&steamid=" + steamid + "&include_appinfo=1&format=json";

            List<Game> games= null;
            var client = new HttpClient();

            var task = await client.GetAsync(path);
            var jsonString = await task.Content.ReadAsStringAsync();
            games = JsonConvert.DeserializeObject<RootObject>(jsonString).response.games;

            return View(games);
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
