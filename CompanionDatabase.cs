using System.Collections.Generic;
using UnityEngine;

public static class CompanionDatabase
{
    public static List<CompanionData> Companions = new List<CompanionData>();

    static CompanionDatabase()
    {
        // 예시: 동료 "Ally"를 등록합니다.
        List<GenericSkill> MariaSkills = new List<GenericSkill>();
        // 스킬은 SkillDatabase에 등록된 스킬을 참조합니다.
       

        Companions.Add(new CompanionData(
            "Maria",      // 이름
            1,           // 레벨
            150,         // HP
            80,          // MP
            10,          // Strength
            8,           // Magic
            12,          // Stamina
            9,           // Speed
            ResistanceType.Normal, 
            ResistanceType.Normal, 
            ResistanceType.Weakness, 
            ResistanceType.Normal, 
            ResistanceType.Normal, 
            ResistanceType.Null, 
            ResistanceType.Absorb,
            MariaSkills   // 초기 스킬 리스트
        ));
    }

    // 이름을 기반으로 동료 데이터를 반환 (대소문자 구분 없이)
    public static CompanionData GetCompanionData(string name)
    {
        return Companions.Find(c => c.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }
}
