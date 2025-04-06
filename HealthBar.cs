using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public CharacterStats characterStats;
    
    // 이전에 출력한 체력 비율
    private float lastLoggedRatio = -1f;
    // 로그 출력 차이 임계값
    private float logThreshold = 0.01f; // 5% 변화 이상일 때만 로그 출력

    void Update()
    {
        if (characterStats != null && healthBarImage != null)
        {
            float ratio = (float)characterStats.currentHP / characterStats.baseHP;
            healthBarImage.fillAmount = ratio;
            
            // 체력 비율의 변화가 임계값 이상일 때만 로그 출력
            if (Mathf.Abs(ratio - lastLoggedRatio) > logThreshold)
            {
                Debug.Log(characterStats.characterName + " 체력 비율: " + ratio);
                lastLoggedRatio = ratio;
            }
        }
    }
}
