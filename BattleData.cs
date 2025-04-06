using UnityEngine;
using System.Collections.Generic;

public class BattleData : MonoBehaviour
{
    public static BattleData Instance;

    // 전투 시 사용할 플레이어 팀 정보
    public List<GameObject> playerTeam;
    // 전투 시 사용할 몬스터 팀 정보
    public MonsterTeamDefinition monsterTeam;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
