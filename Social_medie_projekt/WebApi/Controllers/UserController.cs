﻿namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllAsync();

                if (users.Count == 0)
                {
                    return NoContent();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int userId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser == null || userId != currentUser.User.UserId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthrized" });
                }

                var userResponse = await _userService.GetByIdAsync(userId, currentUser.User.UserId);

                if (userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpPut]
        [Route("{userid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int userId, [FromBody] UserRequest updatedUser)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser == null || userId != currentUser.User.UserId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthrized" });
                }

                var userResponse = await _userService.UpdateAsync(userId, updatedUser);

                if (userResponse == null)
                {
                    return NotFound();
                }

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
