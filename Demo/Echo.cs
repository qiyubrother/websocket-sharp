using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Demo
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage (MessageEventArgs e)
        {
            var name = Context.QueryString["name"];
            Console.WriteLine($"::Received==>{e.Data}");

            var loginData = JsonConvert.DeserializeObject<JsonDataLogin>(e.Data);
            Console.WriteLine($"userName:{loginData.userName}");
            Console.WriteLine($"password:{loginData.password}, Decode:{Base64Helper.Base64Decode(System.Text.Encoding.UTF8, loginData.password)}");
            Console.WriteLine($"hotelId:{loginData.hotelId}");

            //var j = JObject.FromObject(e.Data);
            //Console.WriteLine("::Received==>{0}", j["password"]);

            var obj = new JObject();
            obj.Add(new JProperty("ErrorCode", "0"));
            obj.Add(new JProperty("ErrorMessage", "OK"));
            obj.Add(new JProperty("Token", "testvalue"));
            var msg = obj.ToString();

            System.Threading.Thread.Sleep(2000);
            Send(msg);
            Console.WriteLine($"::Returned==>{msg}");
        }
    }

    public class JsonDataLogin
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string hotelId { get; set; }

    }
}
