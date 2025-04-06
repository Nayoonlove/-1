using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string characterName;
    public int level;
    public int strength;    // 힘
    public int magic;       // 마법
    public int stamina;     // 체력
    public int speed;       // 속도
    
    public int baseHP;      // 최대 체력
    public int baseMP;      // 최대 마나
    public int currentHP;   // 현재 체력
    public int currentMP;   // 현재 마나
    
    public ResistanceType physicalResistance;
    public ResistanceType fireResistance;
    public ResistanceType iceResistance;
    public ResistanceType windResistance;
    public ResistanceType lightningResistance;
    public ResistanceType darkResistance;
    public ResistanceType lightResistance;
    
    public List<Statuseffect> statusEffects;
    public Dictionary<BuffType, int> buffs;
    public int overloadGauge;
    public int overloadThreshold = 100;
    
    [Tooltip("최대 10개까지의 스킬을 할당할 수 있습니다.")]
    public List<GenericSkill> skills;
    
    // 플레이어의 경우, PlayerData를 참조해서 최신 데이터를 반영할 수 있게 함
    public PlayerData playerData;
    
    // UI를 통해 선택 가능한 보너스 스탯 옵션
    public enum BonusStat
    {
        None,
        Strength,
        Magic,
        Stamina,
        Speed
    }
    
    void Awake()
    {
        currentHP = baseHP;
        currentMP = baseMP;
        statusEffects = new List<Statuseffect>();
        buffs = new Dictionary<BuffType, int>();
        if (skills == null)
            skills = new List<GenericSkill>();
        overloadGauge = 0;
    }
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (skills != null && skills.Count > 10)
        {
            Debug.LogWarning("최대 10개까지만 스킬이 허용됩니다. 추가 스킬은 무시됩니다.");
            while (skills.Count > 10)
            {
                skills.RemoveAt(skills.Count - 1);
            }
        }
    }
#endif

    // PlayerData를 기반으로 캐릭터의 스탯을 업데이트합니다.
    public void ApplyPlayerData(PlayerData data)
    {
        playerData = data;
        characterName = data.Name;
        level = data.Level;
        baseHP = data.HP;
        currentHP = data.HP;
        baseMP = data.MP;
        currentMP = data.MP;
        
        strength = data.Strength;
        magic = data.Magic;
        stamina = data.Stamina;
        speed = data.Speed;
        
        physicalResistance = data.physicalResistance;
        fireResistance = data.fireResistance;
        iceResistance = data.iceResistance;
        windResistance = data.windResistance;
        lightningResistance = data.lightningResistance;
        darkResistance = data.darkResistance;
        lightResistance = data.lightResistance;
        
        // Inspector에 스킬이 지정되어 있지 않다면 데이터베이스의 초기 스킬들을 적용
        if (skills == null || skills.Count == 0)
        {
            skills = new List<GenericSkill>(data.initialSkills);
        }
    }

    public void ApplyMonsterData(MonsterData data)
    {
        characterName = data.Name;
        level = data.Level;
        baseHP = data.HP;
        currentHP = data.HP;
        baseMP = data.MP;
        currentMP = data.MP;
        
        strength = data.Strength;
        magic = data.Magic;
        stamina = data.Stamina;
        speed = data.Speed;
        
        physicalResistance = data.physicalResistance;
        fireResistance = data.fireResistance;
        iceResistance = data.iceResistance;
        windResistance = data.windResistance;
        lightningResistance = data.lightningResistance;
        darkResistance = data.darkResistance;
        lightResistance = data.lightResistance;
        
        if (skills == null || skills.Count == 0)
        {
            skills = new List<GenericSkill>(data.initialSkills);
        }
    }
    
    public bool HasStatusEffect(StatusEffectType effectType)
    {
        foreach (var effect in statusEffects)
        {
            if (effect.Type == effectType)
                return true;
        }
        return false;
    }
    
    public int GetPhysicalAttackPower()
    {
        return (strength * 2 + level * 2);
    }
    
    public int GetMagicAttackPower(int baseSkillDamage)
    {
        return (magic * 2 + level * 2 + baseSkillDamage);
    }
    
    public int GetDefense()
    {
        return (stamina + level);
    }
    
    public void ResetOverload()
    {
        overloadGauge = 0;
    }
    
    public void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            statusEffects[i].Tick();
            if (statusEffects[i].IsExpired())
                statusEffects.RemoveAt(i);
        }
    }
    
    public ResistanceType GetResistance(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.Fire:
                return fireResistance;
            case ElementType.Ice:
                return iceResistance;
            case ElementType.Wind:
                return windResistance;
            case ElementType.Electric:
                return lightningResistance;
            case ElementType.Dark:
                return darkResistance;
            case ElementType.Light:
                return lightResistance;
            default:
                return physicalResistance;
        }
    }
    
    public void ApplyStatusEffect(StatusEffectType effectType)
    {
        if (effectType == StatusEffectType.None)
            return;
        
        foreach (var effect in statusEffects)
        {
            if (effect.Type == effectType)
            {
                Debug.Log(characterName + "은(는) 이미 " + effectType + " 상태입니다.");
                return;
            }
        }
        
        statusEffects.Add(new Statuseffect(effectType));
        Debug.Log(characterName + "은(는) " + effectType + " 상태가 되었습니다.");
    }
    
    // 레벨업 메서드: 기본 스탯 +1, 체력 +5, 마나 +3, 그리고 UI를 통한 추가 보너스 스탯 적용 가능
    public void LevelUp(BonusStat bonus = BonusStat.None)
    {
        // 플레이어 데이터가 있다면 업데이트합니다.
        if (playerData != null)
        {
            playerData.Level += 1;
            playerData.Strength += 1;
            playerData.Magic += 1;
            playerData.Stamina += 1;
            playerData.Speed += 1;
            playerData.HP += 5;
            playerData.MP += 3;
            
            if (bonus != BonusStat.None)
            {
                switch (bonus)
                {
                    case BonusStat.Strength:
                        playerData.Strength += 1;
                        break;
                    case BonusStat.Magic:
                        playerData.Magic += 1;
                        break;
                    case BonusStat.Stamina:
                        playerData.Stamina += 1;
                        break;
                    case BonusStat.Speed:
                        playerData.Speed += 1;
                        break;
                }
            }
        }
        
        // CharacterStats 업데이트
        level += 1;
        strength += 1;
        magic += 1;
        stamina += 1;
        speed += 1;
        baseHP += 5;
        baseMP += 3;
        
        if (bonus != BonusStat.None)
        {
            switch (bonus)
            {
                case BonusStat.Strength:
                    strength += 1;
                    break;
                case BonusStat.Magic:
                    magic += 1;
                    break;
                case BonusStat.Stamina:
                    stamina += 1;
                    break;
                case BonusStat.Speed:
                    speed += 1;
                    break;
            }
        }
        
        currentHP = baseHP;
        currentMP = baseMP;
        Debug.Log("레벨업! 새로운 레벨: " + level);
    }
}
