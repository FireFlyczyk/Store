using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Dtos;
using OrderService.Models;

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
        [HttpGet("{id}", Name = "GetOrderbyId")]
        public ActionResult<OrderReadDto> GetOrderById(int id)
        {
            var order = _repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderReadDto>(order));
        }
        [HttpPost]
        public ActionResult<OrderReadDto> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var order = _mapper.Map<Order>(orderCreateDto);
            _repository.CreateOrder(order);
            _repository.SaveChanges();
            var orderReadDto = _mapper.Map<OrderReadDto>(order);
            return CreatedAtRoute("GetOrderbyId", new { Id = orderReadDto.Id }, orderReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult<OrderReadDto> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            var order = _repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            _mapper.Map(orderUpdateDto, order);
            _repository.UpdateOrder(order);
            var orderReadDto = _mapper.Map<OrderReadDto>(order);
            return Ok(orderReadDto);
        }
    }
}

