using BankingAPI.Application.Features.Accounts.Commands;
using BankingAPI.Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountCommand command) {
            command.Id = id;
            var result = await _mediator.Send(command);
            return result ? Ok("Updated Successfully") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id) {
            var result = await _mediator.Send(new DeleteAccountCommand { Id = id });
            return result ? Ok("Deleted Successfully") : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(Guid id) {
            var result = await _mediator.Send(new GetAccountByIdQuery { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAccountsByUserId(string userId) {
            var result = await _mediator.Send(new GetAccountsByUserIdQuery { UserId = userId });
            return Ok(result);
        }
    }
}
