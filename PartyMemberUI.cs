using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PartyMemberUi : MonoBehaviour
{
    public TextMeshProUGUI nameText;    // 파티원 이름
    public Slider hpSlider;             // HP 슬라이더
    public TextMeshProUGUI hpText;      // HP 텍스트 (예: "150/200")
    public Slider mpSlider;             // MP 슬라이더
    public TextMeshProUGUI mpText;      // MP 텍스트 (예: "50/100")
    
    private CharacterStats characterStats;
    public int memberIndex;             // 이 UI가 파티 내에서 몇 번째 멤버인지

    // 파티원 정보를 초기화하는 함수 (멤버 인덱스도 함께 할당)
    public void Setup(CharacterStats stats, int index)
    {
        characterStats = stats;
        memberIndex = index; // 예를 들어, 0이면 첫 번째 멤버
        
        if (characterStats != null)
        {
            nameText.text = characterStats.characterName;
            hpSlider.maxValue = characterStats.baseHP;
            mpSlider.maxValue = characterStats.baseMP;

            UpdateUI();
        }
    }

    void Update()
    {
        if (characterStats != null)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        hpSlider.value = characterStats.currentHP;
        mpSlider.value = characterStats.currentMP;

        hpText.text = characterStats.currentHP.ToString() + "/" + characterStats.baseHP.ToString();
        mpText.text = characterStats.currentMP.ToString() + "/" + characterStats.baseMP.ToString();
    }
}
