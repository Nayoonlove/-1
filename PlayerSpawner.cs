using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Vector3 spawnPosition = new Vector3(-3f, 0f, -3f);

    public void SpawnPlayer(string playerName)
    {
        PlayerData data = PlayerDatabase.GetPlayerData(playerName);
        if (data == null)
        {
            Debug.LogError("해당 플레이어 데이터가 없습니다: " + playerName);
            return;
        }

        GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        CharacterStats stats = player.GetComponent<CharacterStats>();
        if (stats != null)
        {
            // 플레이어 데이터 기반으로 최신 스탯을 적용
            stats.ApplyPlayerData(data);
        }
    }
}
