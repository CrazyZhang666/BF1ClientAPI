using BF1ClientAPI.SDK;
using BF1ClientAPI.Models;

namespace BF1ClientAPI.Controllers;

/// <summary>
/// ���
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class PlayerController : ControllerBase
{
    /// <summary>
    /// ��ȡ�Լ���Ϣ
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// ��ȡս��1�Լ��˺ŵ��������
    /// </remarks>
    [HttpGet]
    [Produces("application/json")]
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
    /// <remarks>
    /// ��ȡս��1��ǰ���������ȫ���������
    /// </remarks>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<List<GamePlayer>> GetGamePlayerList()
    {
        var gamePlayerList = Player.GetGamePlayerList();
        if (gamePlayerList.Count == 0)
            return NotFound();

        for (int i = 0; i < gamePlayerList.Count; i++)
            gamePlayerList[i].Index = i + 1;

        return Ok(gamePlayerList);
    }

    /// <summary>
    /// ��ȡ��Ϸ�ڶ���0����б���Ϣ
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// ��ȡս��1��ǰ�����������ս���������������
    /// </remarks>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<List<GamePlayer>> GetTeam0GamePlayerList()
    {
        var gamePlayerList = Player.GetGamePlayerList();
        if (gamePlayerList.Count == 0)
            return NotFound();

        var team1List = new List<GamePlayer>();
        foreach (var item in gamePlayerList)
        {
            if (item.TeamId != 0)
                continue;

            team1List.Add(item);
        }

        if (team1List.Count == 0)
            return NotFound();

        for (int i = 0; i < team1List.Count; i++)
            team1List[i].Index = i + 1;

        return Ok(team1List);
    }

    /// <summary>
    /// ��ȡ��Ϸ�ڶ���1����б���Ϣ
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// ��ȡս��1��ǰ�������������1��������ݣ��Ѱ��յ÷�˳������
    /// </remarks>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<List<GamePlayer>> GetTeam1GamePlayerList()
    {
        var gamePlayerList = Player.GetGamePlayerList();
        if (gamePlayerList.Count == 0)
            return NotFound();

        var team1List = new List<GamePlayer>();
        foreach (var item in gamePlayerList)
        {
            if (item.TeamId != 1)
                continue;

            team1List.Add(item);
        }

        if (team1List.Count == 0)
            return NotFound();

        team1List.Sort((a, b) => b.Score.CompareTo(a.Score));

        for (int i = 0; i < team1List.Count; i++)
            team1List[i].Index = i + 1;

        return Ok(team1List);
    }

    /// <summary>
    /// ��ȡ��Ϸ�ڶ���2����б���Ϣ
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// ��ȡս��1��ǰ�������������2��������ݣ��Ѱ��յ÷�˳������
    /// </remarks>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<List<GamePlayer>> GetTeam2GamePlayerList()
    {
        var gamePlayerList = Player.GetGamePlayerList();
        if (gamePlayerList.Count == 0)
            return NotFound();

        var team2List = new List<GamePlayer>();
        foreach (var item in gamePlayerList)
        {
            if (item.TeamId != 2)
                continue;

            team2List.Add(item);
        }

        if (team2List.Count == 0)
            return NotFound();

        team2List.Sort((a, b) => b.Score.CompareTo(a.Score));

        for (int i = 0; i < team2List.Count; i++)
            team2List[i].Index = i + 1;

        return Ok(team2List);
    }
}
