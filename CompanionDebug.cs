using UnityEngine;
using System.Collections.Generic;

public class CompanionDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PrintCompanions();
        }
    }

    void PrintCompanions()
    {
        if (TeamManager.instance == null)
        {
            Debug.LogWarning("TeamManager 인스턴스가 없습니다.");
            return;
        }

        // 동료 리스트 가져오기
        List<GameObject> selectedTeam = TeamManager.instance.GetSelectedTeamMembers();
        List<GameObject> candidateTeam = TeamManager.instance.GetCandidateTeamMembers();

        string debugMessage = "선택된 동료: ";
        // 리스트를 순회할 때 null 체크
        for (int i = 0; i < selectedTeam.Count; i++)
        {
            GameObject companion = selectedTeam[i];
            if (companion != null)
            {
                CharacterStats stats = companion.GetComponent<CharacterStats>();
                if (stats != null)
                    debugMessage += stats.characterName + " ";
            }
            else
            {
                debugMessage += "[삭제됨] ";
            }
        }

        debugMessage += "\n후보 동료: ";
        for (int i = 0; i < candidateTeam.Count; i++)
        {
            GameObject companion = candidateTeam[i];
            if (companion != null)
            {
                CharacterStats stats = companion.GetComponent<CharacterStats>();
                if (stats != null)
                    debugMessage += stats.characterName + " ";
            }
            else
            {
                debugMessage += "[삭제됨] ";
            }
        }

        Debug.Log(debugMessage);
    }
}
