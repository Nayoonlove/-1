using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BattleTransition : MonoBehaviour
{
    // Inspector에서 지정한 몬스터 팀 이름 (예: "MariaTeam")
    public string monsterTeamName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonsterTeamDefinition monsterTeam = MonsterTeamManager.Instance.GetTeam(monsterTeamName);
            if (monsterTeam == null)
            {
                Debug.LogWarning("해당 팀 정보가 없습니다: " + monsterTeamName);
                return;
            }

            List<GameObject> playerTeam = TeamManager.instance.GetSelectedTeamMembers();
            if (playerTeam.Count == 0)
            {
                Debug.LogWarning("선발된 플레이어 팀원이 없습니다.");
                return;
            }

            // 각 플레이어 오브젝트에 대해 최신 PlayerData를 적용합니다.
            foreach (GameObject player in playerTeam)
            {
                CharacterStats stats = player.GetComponent<CharacterStats>();
                if (stats != null)
                {
                    // playerData 필드 대신, PlayerDatabase에서 이름을 기반으로 최신 데이터를 가져옵니다.
                    PlayerData updatedData = PlayerDatabase.GetPlayerData(stats.characterName);
                    if (updatedData != null)
                    {
                        stats.ApplyPlayerData(updatedData);
                        Debug.Log("플레이어 데이터 업데이트 완료: " + stats.characterName);
                    }
                    else
                    {
                        Debug.LogWarning("업데이트할 플레이어 데이터가 없습니다: " + stats.characterName);
                    }
                }
            }

            BattleData.Instance.playerTeam = playerTeam;
            BattleData.Instance.monsterTeam = monsterTeam;

            Debug.Log("Battle Transition: 플레이어 팀과 " + monsterTeamName + " 정보를 불러왔습니다.");
            SceneManager.LoadScene("BattleScene");
        }
    }
}
