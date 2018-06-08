using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoutingMachine.Geo
{
    public class Abyss : IAbyss
    {
        public RouteData GetFastestRoute(Location origin, Location destination)
         {
            RouteData route = new RouteData();

            string text = string.Empty;

            string formatedUrl = Global.BaseUrl + "?cmd=route&report=0&mo_o="+ Convert.ToDecimal(origin.y).ToString(new System.Globalization.CultureInfo("en-US")) + "," + Convert.ToDecimal(origin.x).ToString(new System.Globalization.CultureInfo("en-US")) + "&mo_d="+ Convert.ToDecimal(destination.y).ToString(new System.Globalization.CultureInfo("en-US")) + "," + Convert.ToDecimal(destination.x).ToString(new System.Globalization.CultureInfo("en-US")) + "&weight=time&stagegeometry=0&mocs=gdd";

            try
            {
                var request = WebRequest.Create(formatedUrl);//Global.BaseUrl + string.Format("?cmd=route&report=0&mo_o={0},{1}&mo_d={2},{3}&weight=time&stagegeometry=0&mocs=gdd",origin.y, origin.x, destination.y, destination.x )); //40, -3

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();

                        RootObject res = JsonConvert.DeserializeObject<RootObject>(text);

                        if (res != null)
                        {
                            route.time = res.cercalia.route.time;
                            route.distance = res.cercalia.route.dist;
                        }
                    }
                }

            }catch
            {


            }

            return route;
         }

        public Location GetLocation(string address, string city )
        {
            string text = string.Empty;

            Location loc = new Location();

            string formatedUrl = Global.BaseUrl + "?cmd=cand&detcand=1&mode=1&cleanadr=1&adr="+ address +"&ctn=" + city + "&ctryc=esp";
            try
            {
                var request = WebRequest.Create(formatedUrl);//Global.BaseUrl + string.Format("?cmd=route&report=0&mo_o={0},{1}&mo_d={2},{3}&weight=time&stagegeometry=0&mocs=gdd",origin.y, origin.x, destination.y, destination.x )); //40, -3

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();

                        dynamic data = JObject.Parse(text);
                        var res = data.cercalia.candidates.candidate[0].ge.coord;

                        loc.x = res.x;
                        loc.y = res.y;
                    }
                }

            }
            catch
            {

            }

            return loc;
        }

        public RouteData GetDistance(string origin, string destination)
        {
            RouteData route = new RouteData();

            string text = string.Empty;

            string formatedUrl = Global.BaseUrl + "?cmd=route&report=0&ctn_o=" + origin + "&ctn_d=" + destination + "&weight=time&stagegeometry=0&mocs=gdd";

            try
            {
                var request = WebRequest.Create(formatedUrl);//Global.BaseUrl + string.Format("?cmd=route&report=0&mo_o={0},{1}&mo_d={2},{3}&weight=time&stagegeometry=0&mocs=gdd",origin.y, origin.x, destination.y, destination.x )); //40, -3

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();

                        RootObject res = JsonConvert.DeserializeObject<RootObject>(text);

                        if (res != null)
                        {
                            route.time = res.cercalia.route.time;
                            route.distance = res.cercalia.route.dist;
                        }
                    }
                }

            }
            catch
            {


            }

            return route;
        }




    }
}
