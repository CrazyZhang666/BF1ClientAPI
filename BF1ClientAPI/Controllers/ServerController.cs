﻿using BF1ClientAPI.SDK;
using BF1ClientAPI.Utils;
using BF1ClientAPI.Models;

namespace BF1ClientAPI.Controllers;

/// <summary>
/// 服务器
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class ServerController : ControllerBase
{
    /// <summary>
    /// 获取当前服务器数据
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// 获取战地1当前进入服务器的服务器相关数据
    /// 
    /// 征服模式可以获取服务器时间和得分信息，由于是弱指针，有较低概率获取失败
    /// </remarks>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<ServerData> GetServerData()
    {
        var serverData = new ServerData
        {
            // 服务器名称
            Name = Server.GetServerName(),
            // 服务器数字Id
            GameId = Server.GetGameId(),
            // 服务器时间
            Time = Server.GetServerTime(),

            // 服务器游戏模式
            GameMode = Server.GetGameMode(),
            // 服务器地图名称
            MapName = Server.GetMapName()
        };

        // 服务器时间 - 字符串
        serverData.GameTime = GameUtil.GetMMSSStrBySecond(serverData.Time);
        // 服务器游戏模式中文名称
        serverData.GameMode2 = ClientUtil.GetGameMode(serverData.GameMode);
        // 服务器地图中文名称
        serverData.MapName2 = ClientUtil.GetMapChsName(serverData.MapName);
        // 服务器地图预览图
        serverData.MapImage = ClientUtil.GetMapImage(serverData.MapName);

        // 队伍1
        serverData.Team1.Name = ClientUtil.GetTeam1Name(serverData.MapName);
        serverData.Team1.Image = ClientUtil.GetTeam1Image(serverData.MapName);
        // 队伍1分数
        serverData.Team1.MaxScore = Server.GetServerMaxScore();
        serverData.Team1.AllScore = Server.GetTeamScore(1);
        serverData.Team1.ScoreKill = Server.GetTeamKillScore(1);
        serverData.Team1.ScoreFlag = Server.GetTeamFlagScore(1);

        // 队伍2
        serverData.Team2.Name = ClientUtil.GetTeam2Name(serverData.MapName);
        serverData.Team2.Image = ClientUtil.GetTeam2Image(serverData.MapName);
        serverData.Team2.MaxScore = Server.GetServerMaxScore();
        // 队伍2分数
        serverData.Team2.AllScore = Server.GetTeamScore(2);
        serverData.Team2.ScoreKill = Server.GetTeamKillScore(2);
        serverData.Team2.ScoreFlag = Server.GetTeamFlagScore(2);

        return Ok(serverData);
    }
}
