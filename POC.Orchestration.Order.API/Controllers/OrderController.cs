using Microsoft.AspNetCore.Mvc;
using POC.Orchestration.Order.Service.Models;
using POC.Orchestration.Order.Service.Sagas;

namespace POC.Orchestration.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderSaga _createOrderSaga;
        public OrderController(CreateOrderSaga createOrderSaga)
        {
            _createOrderSaga = createOrderSaga;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int orderId)
        {
            return Ok(new OrderModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderModel createOrderModel)
        {
            var order = _createOrderSaga.Create(createOrderModel);
            return Accepted();
        }
    }
}