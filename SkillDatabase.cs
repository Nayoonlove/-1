using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDatabase", menuName = "Skills/Skill Database", order = 1)]
public class SkillDatabase : ScriptableObject
{
    // 최대 500개 이상의 스킬을 Inspector에서 할당할 수 있습니다.
    public List<GenericSkill> skills;
}
