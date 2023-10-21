using computación_en_la_nube___práctica_2C.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace computación_en_la_nube___práctica_2C.Controllers
{
    public static class JsonUtils
    {
        private static readonly JsonSerializerSettings _options
        = new() { NullValueHandling = NullValueHandling.Ignore };

        public static List<ObjectModel> ReadJson()
        {
            using StreamReader reader = new StreamReader(@"data.json");
            var json = reader.ReadToEnd();
            var jarray = JArray.Parse(json);
            List<ObjectModel> objects = new();
            foreach (var item in jarray)
            {
                ObjectModel objectInstance = item.ToObject<ObjectModel>();
                objects.Add(objectInstance);
            }
            return objects;
        }

        public static void WriteJson(List<ObjectModel> list)
        {
            var jsonString = JsonConvert.SerializeObject(list, _options);

            File.WriteAllText(@"data.json", jsonString);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/<ValuesController>/status
        [HttpGet("status")]
        public string GetStatus()
        {
            return "pong";
        }

        // GET api/<ValuesController>/directories
        [HttpGet("directories")]
        public string GetDirectories()
        {
            return JsonUtils.ReadJson().ToString();
        }

        // POST api/<ValuesController>
        [HttpPost("directories")]
        public void Post([FromBody] ObjectModel value)
        {
            List<ObjectModel> list = JsonUtils.ReadJson();
            list.Add(value);
            JsonUtils.WriteJson(list);
        }

        // GET api/<ValuesController>/directories
        [HttpGet("directories/{id}")]
        public string GetDirectories(int id)
        {
            List<ObjectModel> list = JsonUtils.ReadJson();
            return list.First(i => i.id == id).ToString();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("directories/{id}")]
        public void Put(int id, [FromBody] ObjectModel value)
        {
            List<ObjectModel> list = JsonUtils.ReadJson();
            var first = list.First(i=>i.id==id);
            list.Remove(first);
            list.Add(value);
            JsonUtils.WriteJson(list);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("directories/{id}")]
        public void Delete(int id)
        {
            List<ObjectModel> list = JsonUtils.ReadJson();
            var first = list.First(i => i.id == id);
            list.Remove(first);
            JsonUtils.WriteJson(list);
        }
    }
}
