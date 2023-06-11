using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Dtos;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderReadDto>> GetAllOrders()
        {
            var orders = _repository.GetAllOrders();
            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
        }

    }
}