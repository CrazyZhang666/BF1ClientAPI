using BF1ClientAPI.SDK;
using BF1ClientAPI.Models;

namespace BF1ClientAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PlayerController : ControllerBase
{
    /// <summary>
    /// ��ȡ�Լ���Ϣ
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<LocalPlayer> GetLocalPlayer()
    {
        var localPlayer = Player.GetLocalPlayer();
        if (localPlayer is null)
            return NotFound();

        return Ok(localPlayer);
    }

    /// <summary>
    /// ��ȡ��Ϸ������б���Ϣ
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<GamePlayer>> GetGamePlayerList()
    {
        var gamePlayerList = Player.GetGamePlayerList();
        if (gamePlayerList is null)
            return NotFound();

        return Ok(gamePlayerList);
    }
}
