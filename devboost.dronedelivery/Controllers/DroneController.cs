using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devboost.dronedelivery.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devboost.dronedelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await Task.FromResult(Ok("api Ok"));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] Pedido pedido)
        {

            return await Task.FromResult(Ok("api Ok"));
        }

    }
}
