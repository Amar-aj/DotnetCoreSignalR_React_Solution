using Backend.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private IHubContext<DataHub> _hubContext;
    public ChatController(IHubContext<DataHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet("send/test1")]
    public IActionResult GetTestOne(int count)
    {
        var result = _hubContext.Clients.All.SendAsync("chatSection1", test(count.ToString()));
        return Ok(new { Status = "Test 1 Completed" });
    }

    [HttpGet("send/test2")]
    public IActionResult GetTestTwo()
    {
        string randomId = GenerateRandomId();
        var result = _hubContext.Clients.All.SendAsync("chatSection2", randomId);
        return Ok(new { Status = "Test 2 Completed" });
    }

    private string test(string count)
    {
        return $"{count} from method";
    }
    private static string GenerateRandomId(int length = 8)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        char[] idArray = new char[length];

        for (int i = 0; i < length; i++)
        {
            idArray[i] = chars[random.Next(chars.Length)];
        }

        return new string(idArray);
    }
}
