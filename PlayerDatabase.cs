using System.Collections.Generic;

public static class PlayerDatabase
{
    public static List<PlayerData> Players = new List<PlayerData>();

    static PlayerDatabase()
    {
        // 기본 플레이어 추가
        List<GenericSkill> heroSkills = new List<GenericSkill>();
        

        Players.Add(new PlayerData(
            "Hero", 1, 200, 100, 15, 10, 10, 8,
            ResistanceType.Normal, ResistanceType.Normal, ResistanceType.Normal, ResistanceType.Normal, 
            ResistanceType.Normal, ResistanceType.Normal, ResistanceType.Normal, heroSkills));
    }

    public static PlayerData GetPlayerData(string playerName)
    {
    return Players.Find(p => p.Name.Equals(playerName, System.StringComparison.OrdinalIgnoreCase));
    }
}
