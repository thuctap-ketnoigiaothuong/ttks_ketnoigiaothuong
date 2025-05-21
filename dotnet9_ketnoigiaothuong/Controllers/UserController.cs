using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.UserContract;

namespace dotnet9_ketnoigiaothuong.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController() { }
        [HttpPut("update/{user_id}")]
        public async Task<IActionResult> updateUser(int user_id, [FromBody] UserUpdate userUpdate)
        {
            var response = await Provider.UserService.UpdateUserAsync(user_id, userUpdate);
            if (response == null)
            {
                return BadRequest("User not found!");
            }
            return Ok(response);
        }
    }
}
