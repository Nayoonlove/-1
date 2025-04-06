using UnityEngine;
using System.Collections.Generic;

public class PartyStatusUI : MonoBehaviour
{
    [Header("파티원 UI 슬롯 (최대 4개)")]
    // Inspector에서 Party1 Panel 아래에 배치된 파티원 UI 오브젝트들을 할당 (예: 0번부터 3번까지)
    public GameObject[] partyMemberUIObjects;

    // 파티원 정보를 가져올 때 사용할 태그 (예: "Player") - 여러분의 파티 멤버 태그에 맞게 변경하세요.
    public string partyMemberTag = "Player";

    // 실제 파티원 정보를 저장할 리스트
    private List<CharacterStats> partyMembers = new List<CharacterStats>();

    void Start()
    {
        // 파티원 UI 슬롯이 제대로 할당되었는지 확인
        if (partyMemberUIObjects == null || partyMemberUIObjects.Length == 0)
        {
            Debug.LogError("PartyMemberUIObjects 배열이 비어 있습니다. Inspector에서 4개의 UI 슬롯을 할당하세요.");
            return;
        }

        // 우선 모든 UI 슬롯을 비활성화
        foreach (GameObject uiObj in partyMemberUIObjects)
        {
            uiObj.SetActive(false);
        }

        // 파티원 태그를 이용해 실제 파티원(플레이어) 오브젝트들을 찾습니다.
        GameObject[] foundMembers = GameObject.FindGameObjectsWithTag(partyMemberTag);
        foreach (GameObject member in foundMembers)
        {
            CharacterStats stats = member.GetComponent<CharacterStats>();
            if (stats != null)
            {
                partyMembers.Add(stats);
            }
        }

        // 파티원 수에 따라 UI 슬롯을 활성화하고, 각 슬롯에 해당 파티원의 정보를 연결합니다.
        int count = Mathf.Min(partyMembers.Count, partyMemberUIObjects.Length);
        for (int i = 0; i < count; i++)
        {
            // UI 슬롯 활성화
            partyMemberUIObjects[i].SetActive(true);

            // UI 슬롯에 붙어있는 PartyMemberUi 스크립트를 가져와서 Setup() 호출
            PartyMemberUi memberUi = partyMemberUIObjects[i].GetComponent<PartyMemberUi>();
            if (memberUi != null)
            {
                // 각 파티원에 대해 CharacterStats와 인덱스를 전달합니다.
                memberUi.Setup(partyMembers[i], i);
            }
            else
            {
                Debug.LogWarning("UI 슬롯 " + i + "에 PartyMemberUi 스크립트가 없습니다.");
            }
        }
    }
}
