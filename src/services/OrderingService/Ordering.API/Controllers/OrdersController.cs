using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.UseCases.CreateOrder;
using Ordering.Application.UseCases.GetOrders;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
     private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid customerId)
    {
        var result = await _mediator.Send(new GetOrdersQuery(customerId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command )
    {
        var orderId = await _mediator.Send(command);
        return Ok(new { OrderId = orderId });
    }
}
