using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using devboost.dronedelivery.DTO;
using devboost.dronedelivery.Model;
using devboost.dronedelivery.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devboost.dronedelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        readonly PedidoService _pedidoService;

        public DroneController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await Task.FromResult(Ok("api Ok"));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] PedidoDTO pedidoDto)
        {
            try
            {
               
                var result = await _pedidoService.RealizarPedido(pedidoDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

    }
}
