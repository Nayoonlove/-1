using System.Collections.Generic;
using UnityEngine;

public class MonsterTeamManager : MonoBehaviour {
    public static MonsterTeamManager Instance;
    public MonsterTeamDatabase teamDatabase;  // 에디터에서 할당 (MonsterTeamDatabase asset)
    
    private Dictionary<string, MonsterTeamDefinition> teamDictionary;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildDictionary();
        } else {
            Destroy(gameObject);
        }
    }

    // 데이터베이스의 팀 정의를 딕셔너리로 구성 (추가/수정가 용이)
    private void BuildDictionary() {
        teamDictionary = new Dictionary<string, MonsterTeamDefinition>();
        foreach (var teamDef in teamDatabase.teamDefinitions) {
            if (!teamDictionary.ContainsKey(teamDef.teamName)) {
                teamDictionary.Add(teamDef.teamName, teamDef);
            } else {
                Debug.LogWarning("중복된 팀 이름 발견: " + teamDef.teamName);
            }
        }
    }

    // 팀 이름을 키로 팀 정보를 반환 (없는 경우 null)
    public MonsterTeamDefinition GetTeam(string teamName) {
        MonsterTeamDefinition team;
        if (teamDictionary.TryGetValue(teamName, out team)) {
            return team;
        }
        Debug.LogWarning("팀 정보가 없습니다: " + teamName);
        return null;
    }
}
