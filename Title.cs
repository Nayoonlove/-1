using System.Collections.Generic;

// Title 클래스는 칭호의 이름, 설명 및 각 속성별 효과를 관리합니다.
public class Title
{
    public string titleName;
    public string description;
    public Dictionary<ElementType, float> attackBonuses;
    public Dictionary<ElementType, float> damageReductions;

    public Title(string titleName, string description)
    {
        this.titleName = titleName;
        this.description = description;
        attackBonuses = new Dictionary<ElementType, float>();
        damageReductions = new Dictionary<ElementType, float>();
    }

    public void SetAttackBonus(ElementType element, float bonus)
    {
        attackBonuses[element] = bonus;
    }

    public void SetDamageReduction(ElementType element, float reduction)
    {
        damageReductions[element] = reduction;
    }

    public float GetAttackBonus(ElementType element)
    {
        return attackBonuses.ContainsKey(element) ? attackBonuses[element] : 0f;
    }

    public float GetDamageReduction(ElementType element)
    {
        return damageReductions.ContainsKey(element) ? damageReductions[element] : 0f;
    }
}
