using UnityEngine;  // Random.Range 사용을 위해

public class Statuseffect
{
    public StatusEffectType Type { get; private set; }
    public int Duration { get; private set; }

    // 생성자: 상태이상 효과의 종류를 받아, 지속 턴을 자동 결정
    public Statuseffect(StatusEffectType type)
    {
        Type = type;
        Duration = DetermineDuration(type);
    }

    // 각 상태이상 효과에 따른 지속 턴을 결정하는 메서드
    private int DetermineDuration(StatusEffectType type)
    {
        switch (type)
        {
            case StatusEffectType.Poison:
                return 5; // 독은 고정 5턴
            case StatusEffectType.Ice:
                return Random.Range(1, 4); // 1~3턴 (Random.Range의 상한은 제외)
            case StatusEffectType.Confusion:
                return Random.Range(1, 3); // 1~2턴
            case StatusEffectType.Charm:
                return Random.Range(1, 3); // 1~2턴
            case StatusEffectType.Seal:
                return Random.Range(1, 4); // 1~3턴
            case StatusEffectType.Paralysis:
                return Random.Range(1, 4); // 1~3턴
            default:
                return 0;
        }
    }

    // 매 턴마다 호출하여 효과 지속 시간을 1 턴 감소시킵니다.
    public void Tick()
    {
        Duration = Mathf.Max(Duration - 1, 0);
    }

    // 효과가 종료되었는지 확인하는 메서드
    public bool IsExpired()
    {
        return Duration <= 0;
    }
}