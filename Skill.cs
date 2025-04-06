using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName; // 스킬 이름
    public SkillType skillType; // 스킬 타입 (공격, 버프, 디버프 등)
    public ElementType elementType; // 🔥 속성 추가

    public abstract void Execute(CharacterStats user, CharacterStats target);
}
