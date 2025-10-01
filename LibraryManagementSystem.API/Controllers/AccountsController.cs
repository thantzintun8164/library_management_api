using LibraryManagementSystem.API.Common.Responses;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Common.Results;
using LibraryManagementSystem.Application.Features.Account.Commands.AddToRole;
using LibraryManagementSystem.Application.Features.Account.Commands.ChangePassword;
using LibraryManagementSystem.Application.Features.Account.Commands.ForgetPassword;
using LibraryManagementSystem.Application.Features.Account.Commands.RemoveFromRole;
using LibraryManagementSystem.Application.Features.Account.Commands.ResetPassword;
using LibraryManagementSystem.Application.Features.Account.Commands.UpdateProfile;
using LibraryManagementSystem.Application.Features.Account.DTOs;
using LibraryManagementSystem.Application.Features.Account.Queries.GetAllUser;
using LibraryManagementSystem.Application.Features.Account.Queries.GetCurrentUser;
using LibraryManagementSystem.Application.Features.Account.Queries.GetUserById;
using LibraryManagementSystem.Application.Features.Account.Queries.GetUsersWithPagination;
using LibraryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Manage User Accounts: list, get by id, profile, update profile, change password, forget/reset password")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string AdminRole = nameof(Role.Admin);
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("profile")]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get Current Profile", Description = "Retrieve the profile details of the currently authenticated user.")]
        public async Task<IActionResult> GetCurrentProfile()
        {
            var userDto = await _mediator.Send(new GetCurrentUserQuery());
            return Ok(userDto);
        }




        [HttpGet("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get User by Id", Description = "Retrieve a user's profile by id (Admin only).")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var userDto = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(userDto);
        }




        [HttpGet]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<List<UserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get All Users", Description = "Retrieve all users (Admin only).")]
        public async Task<IActionResult> GetAll()
        {
            var userDtos = await _mediator.Send(new GetAllUserQuery());
            return Ok(userDtos);
        }


        [HttpGet("pagination")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<PaginatedResult<UserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get users with pagination", Description = "Retrieve users using pagination parameters 'pageNumber' and 'pageSize' (Admin only).")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var paginatedResult = await _mediator.Send(new GetUsersWithPaginationQuery(pageNumber, pageSize, baseUrl));
            return Ok(paginatedResult);
        }


        [HttpPut("profile")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Update Profile", Description = "Update the profile information of the currently authenticated user.")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            await _mediator.Send(command);
            return Ok(Result.Success("Profile was updated successfully."));
        }



        [HttpPost("change-password")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Change Password", Description = "Change the current user's password by providing the old and new password.")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok(Result.Success("Password was changed successfully."));
        }




        [HttpPost("add-role")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Assign Role to User", Description = "Assign a new role to a user (Admin only).")]
        public async Task<IActionResult> AddToRole([FromBody] AddToRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok(Result.Success("User was successfully assigned to the role."));
        }



        [HttpPost("remove-role")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Remove Role from User", Description = "Remove an assigned role from a user (Admin only).")]
        public async Task<IActionResult> RemoveFromRole([FromBody] RemoveFromRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok(Result.Success("User was successfully removed from the role."));
        }


        [AllowAnonymous]
        [HttpPost("forget-password")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Forget Password", Description = "Send a password reset link to the user's registered email address.")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok(Result.Success("Password reset link was sent successfully. Check your email."));
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Reset Password", Description = "Reset a user's password using the reset token received by email.")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok(Result.Success("Password was reset successfully."));
        }


    }
}
