using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("Admins")]
    [Authorize]
    public IActionResult AdminsEndpoint()
    {
        var currentUser = GetCurrentUser();
        if (currentUser != null) {
            return Ok($"Hi {currentUser.Username}, you are an {currentUser.Role}");
        } else {
            return Unauthorized("You need to be Administrator !");
        }
    }

    [HttpGet("Sellers")]
    [Authorize(Roles = "Seller")]
    public IActionResult SellersEndpoint()
    {
        var currentUser = GetCurrentUser();
        if (currentUser != null) {
            return Ok($"Hi {currentUser.Username}, you are a {currentUser.Role}");
        } else {
            return Unauthorized("You need to be Seller !");
        }
    }

    [HttpGet("AdminsAndSellers")]
    [Authorize(Roles = "Administrator,Seller")]
    public IActionResult AdminsAndSellersEndpoint()
    {
        var currentUser = GetCurrentUser();
        if (currentUser != null) {
            return Ok($"Hi {currentUser.Username}, you are an {currentUser.Role}");
        } else {
            return Unauthorized("You need to be Administrator or Seller !");
        }
    }

    [HttpGet("Public")]
    public IActionResult Public()
    {
        return Ok("Hi, you're on public property");
    }

    private UserModel? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return new UserModel
            {
                Username     = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                Role         = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
            };
        }
        return null;
    }
}
