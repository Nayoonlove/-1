using UnityEngine;

public class MonsterCard
{
    public string cardName;
    public Trait trait; // 카드에 부여된 특성

    public MonsterCard(string cardName, Trait trait)
    {
        this.cardName = cardName;
        this.trait = trait;
    }

    // 카드의 특성을 발동시키는 메서드 (예시)
    public void ActivateTrait(CharacterStats owner)
    {
        // 특성 효과를 발동하는 로직 (여기서는 단순 로그 출력)
        Debug.Log("카드 [" + cardName + "]의 특성 [" + trait.Name + "] 발동: " + trait.Description);
        // 예를 들어, 오버로드 상태에서 추가 공격력 보너스나 기타 효과 적용 가능
        // owner.strength += 5;  // 예시: 공격력 5 증가
    }
}
