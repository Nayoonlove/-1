using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Vector3 spawnPosition = new Vector3(3f, 0f, 3f);

    public void SpawnMonster(string monsterName)
    {
        MonsterData data = MonsterDatabase.GetMonsterData(monsterName);
        if (data == null)
        {
            Debug.LogError("해당 몬스터 데이터가 없습니다: " + monsterName);
            return;
        }

        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        CharacterStats stats = monster.GetComponent<CharacterStats>();
        if (stats != null)
        {
            // 몬스터 데이터 기반으로 최신 스탯을 적용
            stats.ApplyMonsterData(data);
        }
    }
}
