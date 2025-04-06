using UnityEngine;

public class BattleSpawner : MonoBehaviour
{
    // 플레이어 팀 소환 좌표 (예: 화면 왼쪽 아래에 배치)
    private Vector3[] playerSpawnPositions = new Vector3[]
    {
        new Vector3(16f, 2f, 20f),
        new Vector3(16f, 2f, 15f),
        new Vector3(16f, 2f, 10f),
        new Vector3(16f, 2f, 5f)
    };

    // 몬스터 팀 소환 좌표 (예: 화면 오른쪽 위에 배치)
    private Vector3[] enemySpawnPositions = new Vector3[]
    {
        new Vector3(25f, 2f, 25f),
        new Vector3(25f, 2f, 20f),
        new Vector3(25f, 2f, 15f),
        new Vector3(25f, 2f, 10f),
        new Vector3(25f, 2f, 5f),
        new Vector3(25f, 2f, 0f)
    };

    // EnemyHealthBarManager 컴포넌트를 Inspector에서 할당 (적 체력바 등록에 사용)
    public EnemyHealthBarManager enemyHealthBarManager;

    void Start()
    {
        SpawnPlayerTeam();
        SpawnEnemyTeam();
    }

    // 플레이어 팀 소환
    void SpawnPlayerTeam()
    {
        // 이미 플레이어 오브젝트가 씬에 있다면 새로 생성하지 않음.
        if(GameObject.FindWithTag("Player") != null)
        {
            Debug.Log("플레이어 오브젝트가 이미 존재합니다. 새로 생성하지 않습니다.");
            return;
        }
        
        // BattleData에 저장된 플레이어 팀 정보를 가져옴.
        var playerTeam = BattleData.Instance.playerTeam;
        if (playerTeam == null || playerTeam.Count == 0)
        {
            Debug.LogWarning("플레이어 팀 정보가 없습니다.");
            return;
        }

        // 각 플레이어 프리팹을 지정된 스폰 좌표에 생성
        for (int i = 0; i < playerTeam.Count; i++)
        {
            if (i >= playerSpawnPositions.Length)
            {
                Debug.LogWarning("플레이어 스폰 포인트가 부족합니다.");
                break;
            }
            Instantiate(playerTeam[i], playerSpawnPositions[i], Quaternion.identity);
        }
    }

    // 몬스터 팀 소환
    void SpawnEnemyTeam()
    {
        // BattleData에 저장된 몬스터 팀 정보를 가져옴.
        var monsterTeam = BattleData.Instance.monsterTeam;
        if (monsterTeam == null)
        {
            Debug.LogWarning("몬스터 팀 정보가 없습니다.");
            return;
        }

        int spawnIndex = 0;
        // MonsterTeamDefinition의 각 팀원(member)에 대해 minCount ~ maxCount 범위 내에서 소환
        foreach (var member in monsterTeam.teamMembers)
        {
            // Random.Range 두 번째 파라미터는 상한(미포함)이므로 +1
            int count = Random.Range(member.minCount, member.maxCount + 1);
            for (int i = 0; i < count; i++)
            {
                if (spawnIndex >= enemySpawnPositions.Length)
                {
                    Debug.LogWarning("적 스폰 포인트가 부족합니다.");
                    return;
                }
                // 적 생성
                GameObject enemy = Instantiate(member.monsterPrefab, enemySpawnPositions[spawnIndex], Quaternion.identity);
                spawnIndex++;

                // 생성된 적에 대해 EnemyHealthBarManager를 통해 체력바 등록
                if (enemyHealthBarManager != null)
                {
                    enemyHealthBarManager.RegisterEnemy(enemy);
                }
            }
        }
    }
}
