using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterTeamMember {
    public string monsterName;              // 이름 (참고용)
    public GameObject monsterPrefab;        // 스폰할 프리팹 (에디터 할당)
    public int minCount;                    // 최소 등장 수
    public int maxCount;                    // 최대 등장 수
}

[System.Serializable]
public class MonsterTeamDefinition {
    public string teamName;                 // 팀 이름 (예: "MariaTeam")
    public List<MonsterTeamMember> teamMembers; // 팀에 포함된 멤버 리스트
}