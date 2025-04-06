using UnityEngine; using UnityEngine.UI; using System.Collections.Generic;

public class BattleUi : MonoBehaviour { // 각 체력바 UI 요소에 붙은 HealthBar 컴포넌트를 리스트로 관리 public List<HealthBar> healthBars;
void Update()
{
    // 각 HealthBar 컴포넌트는 자신의 Update에서 자동으로 갱신되므로,
    // 추가적으로 전체 UI 업데이트가 필요하다면 이곳에서 처리할 수 있습니다.
}
}