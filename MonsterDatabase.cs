using System.Collections.Generic;

public static class MonsterDatabase
{
    public static List<MonsterData> Monsters = new List<MonsterData>();

    static MonsterDatabase()
    {
        // 몬스터 등록 예시
        List<GenericSkill> demonSkills = new List<GenericSkill>();
        

        Monsters.Add(new MonsterData(
            "Maria", 1, 100, 50, 20, 5, 8, 6,
            ResistanceType.Weakness, ResistanceType.Normal, ResistanceType.Half, ResistanceType.Null,
            ResistanceType.Normal, ResistanceType.Absorb, ResistanceType.Normal, demonSkills));
    }

    public static MonsterData GetMonsterData(string monsterName)
    {
        return Monsters.Find(m => m.Name == monsterName);
    }
}
