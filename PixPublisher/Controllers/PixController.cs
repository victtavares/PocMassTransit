using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Events;
using MassTransit;

namespace PixPublisher.Controllers {
    public class PixRequest {
        [Required]
        public long Value { get; set; }
    }
    
    [Route("[controller]")]
    public class PixController : ControllerBase {
        private readonly IBus _bus;

        public PixController(IBus bus) {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> PayPix([FromBody] PixRequest request) {
            if (request != null) {
                var endpoint = await _bus.GetSendEndpoint(new Uri("queue:pay-pix"));
                
                await endpoint.Send<IPayPix>(new {
                    Id = NewId.NextGuid(),
                   request.Value
                });
                
                return Ok();
            }
            
            return BadRequest();
        }
    }
    
}