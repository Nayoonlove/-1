using UnityEngine;

public class CompanionSpawner : MonoBehaviour
{
    public GameObject companionPrefab; // 동료 프리팹 (Inspector에서 할당)
    public Vector3 spawnPosition = new Vector3(0f, 0f, 0f);

    // companionName: CompanionDatabase에 등록된 이름 (예: "Ally")
    public void SpawnCompanion(string companionName)
    {
        CompanionData data = CompanionDatabase.GetCompanionData(companionName);
        if (data == null)
        {
            Debug.LogError("해당 동료 데이터가 없습니다: " + companionName);
            return;
        }

        // 동료 프리팹 인스턴스화
        GameObject companion = Instantiate(companionPrefab, spawnPosition, Quaternion.identity);
        CharacterStats stats = companion.GetComponent<CharacterStats>();
        if (stats != null)
        {
            // 동료 데이터 기반으로 스탯 업데이트 (PlayerSpawner, MonsterSpawner와 유사하게)
            stats.characterName = data.Name;
            stats.level = data.Level;
            stats.baseHP = data.HP;
            stats.currentHP = data.HP;
            stats.baseMP = data.MP;
            stats.currentMP = data.MP;
            stats.strength = data.Strength;
            stats.magic = data.Magic;
            stats.stamina = data.Stamina;
            stats.speed = data.Speed;
            stats.physicalResistance = data.physicalResistance;
            stats.fireResistance = data.fireResistance;
            stats.iceResistance = data.iceResistance;
            stats.windResistance = data.windResistance;
            stats.lightningResistance = data.lightningResistance;
            stats.darkResistance = data.darkResistance;
            stats.lightResistance = data.lightResistance;
            stats.skills = new System.Collections.Generic.List<GenericSkill>(data.initialSkills);
        }

        // TeamManager에 후보 팀원으로 추가 (이미 TeamManager가 구현되어 있어 이를 활용)
        if (TeamManager.instance != null)
        {
            TeamManager.instance.AddCandidateTeamMember(companion);
        }
        else
        {
            Debug.LogWarning("TeamManager 인스턴스가 없습니다. 동료를 팀에 추가할 수 없습니다.");
        }
    }
}
