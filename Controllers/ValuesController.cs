using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPIApp.Models;
//using WebAPIApp.Models;

namespace WebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }

        // GET api/values
        [HttpGet("")]
        public ActionResult Get()
        {
            // var value = _context.Values.ToList();
            List<Value> ValuesList = _context.Values.Select(
                v => new Value()
                {
                    Id = v.Id,
                    Name = v.Name
                }
            ).ToList();

            foreach (Value val in ValuesList)
            {
                Console.WriteLine($"Id : {val.Id}, Name : {val.Name}");
            }
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ValuesList));

            return Ok(ValuesList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult GetstringById(int id)
        {
            // var value = _context.Values.FirstOrDefault(x => x.Id == id);
            try
            {
                Value value = (from u in _context.Values
                               where u.Id == id
                               select new Value
                               {
                                   Id = u.Id,
                                   Name = u.Name
                               }).FirstOrDefault();
                if (value != null)
                {
                    Console.WriteLine(value);
                    return Ok(value);
                }
                else
                {
                    var response = new
                    {
                        Error = true,
                        Message = "No data found!"
                    };
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        // POST api/values
        [HttpPost("")]
        public ActionResult PostValue(Value value)
        {
            Console.WriteLine(value);
            var result = new object();
            try
            {
                Value val = new Value();
                val.Id = value.Id;
                val.Name = value.Name;
                _context.Values.Add(val);
                _context.SaveChanges();

                Console.WriteLine(val);

                result = new
                {
                    error = false,
                    message = "Post method success!"
                };

                return Ok(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = new
                {
                    error = true,
                    message = "Post method error!"
                };
                // throw;
                return Ok(result);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult PutValueById(int id, [FromBody] Value value)
        {
            var result = new object();
            try
            {

                Value val = _context.Values.Where(v => v.Id == id).FirstOrDefault();
                Console.WriteLine(value);
                Console.WriteLine(val);
                if (val != null)
                {
                    val.Id = value.Id;
                    val.Name = value.Name;
                    _context.SaveChanges();

                    result = new
                    {
                        error = false,
                        message = "Put method success!"
                    };
                    return Ok(result);
                }
                else
                {
                    result = new
                    {
                        error = true,
                        message = "No data found!"
                    };
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // throw;
                result = new
                {
                    error = true,
                    message = "Put method error!"
                };
                return Ok(result);
            }

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult DeleteValueById(int id)
        {
            var result = new object();
            try
            {
                Value val = _context.Values.Where(v => v.Id == id).FirstOrDefault();
                if (val != null)
                {
                    _context.Values.Remove(val);
                    _context.SaveChanges();

                    result = new
                    {
                        error = false,
                        message = "Put method success!"
                    };
                    return Ok(result);
                }
                else
                {
                    result = new
                    {
                        error = true,
                        message = "No data found!"
                    };
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // throw;
                result = new
                {
                    error = true,
                    message = "Delete method error!"
                };
                return Ok(result);
            }
        }
    }
}