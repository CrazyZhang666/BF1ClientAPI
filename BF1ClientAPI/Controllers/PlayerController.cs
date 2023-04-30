using BF1ClientAPI.SDK;
using BF1ClientAPI.Models;

namespace BF1ClientAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PlayerController : ControllerBase
{
    /// <summary>
    /// ��ȡ���������ϸ���Լ���
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<LocalPlayer> GetLocalPlayer()
    {
        var localPlayer = Player.GetPlayerLocal();
        if (localPlayer is null)
            return NotFound();

        return Ok(localPlayer);
    }
}
