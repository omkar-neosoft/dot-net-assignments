using BankingAPI.Application.Features.Transactions.Commands;
using BankingAPI.Application.Features.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id) {
            var result = await _mediator.Send(new DeleteTransactionCommand { Id = id });
            return result ? Ok("Deleted Successfully") : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id) {
            var result = await _mediator.Send(new GetTransactionByIdQuery { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetTransactionsByAccountId(Guid accountId) {
            var result = await _mediator.Send(new GetTransactionsByAccountIdQuery { AccountId = accountId });
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(string userId) {
            var result = await _mediator.Send(new GetTransactionsByUserIdQuery { UserId = userId });
            return Ok(result);
        }
    }
}
