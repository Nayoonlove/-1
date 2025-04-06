using System.Collections.Generic;

public class MonsterData
{
    public string Name;
    public int Level;
    public int HP;
    public int MP;
    public int Strength;
    public int Magic;
    public int Stamina;
    public int Speed;

    // 저항 추가 (속성 X, 내성만 적용)
    public ResistanceType physicalResistance;
    public ResistanceType fireResistance;
    public ResistanceType iceResistance;
    public ResistanceType windResistance;
    public ResistanceType lightningResistance;
    public ResistanceType darkResistance;
    public ResistanceType lightResistance;

    public List<GenericSkill> initialSkills;

    public MonsterData(
        string name, int level, int hp, int mp, int strength, int magic, int stamina, int speed,
        ResistanceType physical, ResistanceType fire, ResistanceType ice, ResistanceType wind, 
        ResistanceType lightning, ResistanceType dark, ResistanceType light, List<GenericSkill> skills)
    {
        Name = name;
        Level = level;
        HP = hp;
        MP = mp;
        Strength = strength;
        Magic = magic;
        Stamina = stamina;
        Speed = speed;
        initialSkills = skills ?? new List<GenericSkill>();

        // 내성 값 초기화
        physicalResistance = physical;
        fireResistance = fire;
        iceResistance = ice;
        windResistance = wind;
        lightningResistance = lightning;
        darkResistance = dark;
        lightResistance = light;
    }
}
