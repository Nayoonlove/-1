using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyHealthBarManager : MonoBehaviour
{
    // 적 체력바 프리팹 (위에서 만든 HealthBar 프리팹)
    public GameObject enemyHealthBarPrefab;
    // 생성된 체력바를 모아둘 부모 컨테이너 (예: Canvas 내 Panel)
    public Transform healthBarContainer;

    // 적과 체력바의 매핑을 위한 딕셔너리
    private Dictionary<GameObject, GameObject> enemyHealthBars = new Dictionary<GameObject, GameObject>();

    // 적이 생성될 때 호출되는 함수
    public void RegisterEnemy(GameObject enemy)
    {
        // 적 체력바 프리팹을 부모 컨테이너 아래에 인스턴스화
        GameObject healthBarObj = Instantiate(enemyHealthBarPrefab, healthBarContainer);
        
        // 생성된 체력바의 HealthBar 스크립트에 적의 CharacterStats 연결
        HealthBar healthBar = healthBarObj.GetComponent<HealthBar>();
        if (healthBar != null)
        {
            CharacterStats stats = enemy.GetComponent<CharacterStats>();
            healthBar.characterStats = stats;
        }
        
        // 매핑 정보를 저장 (필요 시, 적이 죽었을 때 체력바 제거 등에 사용)
        enemyHealthBars.Add(enemy, healthBarObj);
    }

    // 적이 제거될 때 체력바도 제거하는 함수 (옵션)
    public void RemoveEnemy(GameObject enemy)
    {
        if (enemyHealthBars.ContainsKey(enemy))
        {
            Destroy(enemyHealthBars[enemy]);
            enemyHealthBars.Remove(enemy);
        }
    }
}
