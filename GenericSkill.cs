using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Generic Skill", order = 1)]
public class GenericSkill : Skill
{
    public int Damage;
    public int ManaCost;
    public float statusEffectChance;
    public StatusEffectType statusEffect;

    public override void Execute(CharacterStats user, CharacterStats target)
    {
        if (user.currentMP < ManaCost)
        {
            Debug.Log(user.characterName + "의 MP가 부족하여 " + skillName + " 스킬을 사용할 수 없습니다.");
            return;
        }
        
        user.currentMP -= ManaCost;
        int calculatedDamage = Damage;
        
        // 스킬 타입에 따라 데미지 계산
        if (skillType == SkillType.Magical)
        {
            calculatedDamage = user.GetMagicAttackPower(Damage) - target.GetDefense();
        }
        else if (skillType == SkillType.Physical)
        {
            calculatedDamage = user.GetPhysicalAttackPower() - target.GetDefense();
        }
        
        if (calculatedDamage < 0)
            calculatedDamage = 0;
        
        // 대상의 해당 속성(스킬의 elementType)이 약점이면 1.5배 데미지 적용
        if (target.GetResistance(elementType) == ResistanceType.Weakness)
        {
            calculatedDamage = (int)(calculatedDamage * 1.5f);
            Debug.Log("약점 공격! " + user.characterName + "의 " + skillName + " 스킬이 1.5배 데미지를 입힙니다.");
        }
        
        target.currentHP -= calculatedDamage;
        Debug.Log(user.characterName + "가 " + skillName + " 스킬을 사용하여 " + target.characterName + "에게 " + calculatedDamage + "의 데미지를 입혔습니다.");
        
        if (statusEffectChance > 0 && Random.value < statusEffectChance)
        {
            target.ApplyStatusEffect(statusEffect);
            Debug.Log(target.characterName + "에게 " + statusEffect.ToString() + " 상태 효과가 부여되었습니다.");
        }
    }
}
