using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace app1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("create/{id}")]
        public IActionResult CreateNewJsonFile(int id)
        {
            string file = id.ToString() + ".json";
            if (!System.IO.File.Exists(file))
            {
                var _data = new MyData
                {
                    Data = "Cool " + id
                };
                string json = JsonSerializer.Serialize(_data);
                System.IO.File.WriteAllText(file, json);
            }

            if (System.IO.File.Exists(file)) return Ok("Created");

            return BadRequest("Not created");
        }

        [HttpGet("get/{id}")]
        public IActionResult GetJsonFile(int id)
        {
            string file = id.ToString() + ".json";
            if (System.IO.File.Exists(file))
            {
                string jsonString = System.IO.File.ReadAllText(file);
                MyData u = JsonSerializer.Deserialize<MyData>(jsonString);
                return Ok(u.Data);
            }

            return BadRequest("Not found?");
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteJsonFile(int id)
        {
            string file = id.ToString() + ".json";
            if (!System.IO.File.Exists(file)) return NoContent();
            
            string jsonString = System.IO.File.ReadAllText(file);
            MyData u = JsonSerializer.Deserialize<MyData>(jsonString);
            
            System.IO.File.Delete(file);
            return Ok(u);
        }
        public class MyData {
            public string Data { get; set; }
        }
    }
}
