using UnityEngine;

[CreateAssetMenu(menuName = "Skills/AttackSkill")]
public class AttackSkill : Skill
{
    public AttackSkillCategory attackCategory; // StrengthBased (물리) 또는 MagicBased (마법)
    public int baseSkillDamage;   // 마법 공격의 경우 스킬 고유 데미지

    public override void Execute(CharacterStats user, CharacterStats target)
    {
        int damage = 0;
        if (attackCategory == AttackSkillCategory.StrengthBased)
        {
            // 물리 공격: (힘 * 2 + 레벨 * 2) - (체 + 레벨)
            damage = user.GetPhysicalAttackPower() - target.GetDefense();
        }
        else if (attackCategory == AttackSkillCategory.MagicBased)
        {
            // 마법 공격: (마 * 2 + 레벨 * 2 + 스킬 데미지) - (체 + 레벨)
            damage = user.GetMagicAttackPower(baseSkillDamage) - target.GetDefense();
        }
        if (damage < 0)
            damage = 0;
        target.currentHP -= damage;
        Debug.Log(user.characterName + "가 " + damage + "의 데미지를 " + target.characterName + "에게 입혔습니다.");
    }
}
