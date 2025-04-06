using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    private List<CharacterStats> playerTeam;
    private List<CharacterStats> enemyTeam;

    // 초기 플레이어 턴 시작을 위한 플래그
    private bool isPlayerTurn = true;
    private int selectedTargetIndex = 0; // 선택된 적 인덱스
    private TurnState currentTurnState;  // 현재 턴 상태

    // 행동 종류를 구분하기 위한 열거형
    private enum ActionType
    {
        None,
        NormalAttack,
        Skill,
        Capture   // 동료 영입 액션
    }
    private ActionType selectedAction = ActionType.None;
    private int selectedSkillIndex = -1; // 키패드 1~8번으로 선택한 스킬의 인덱스

    // 턴 상태 열거형
    private enum TurnState
    {
        Waiting,         // 입력 대기
        SelectingAction, // 행동 선택 중 (일반 공격, 스킬, 동료 영입)
        SelectingTarget, // 대상 선택 중 (WS키로 이동, H키로 확정)
        ExecutingAction  // 행동 실행 중
    }

    // 동료 영입 시 사용할 동료용 프리팹과 소환 위치
    public GameObject companionPrefab; // 예: Maria_Colleague 프리팹 (Inspector에 할당)
    public Vector3 companionSpawnPosition = new Vector3(16f, 2f, 20f); // 원하는 소환 위치

    void Start()
    {
        playerTeam = new List<CharacterStats>();
        enemyTeam = new List<CharacterStats>();

        DisablePlayerMovement();

        // "Player" 태그가 붙은 모든 오브젝트의 CharacterStats를 가져옴
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            CharacterStats stats = go.GetComponent<CharacterStats>();
            if (stats != null)
                playerTeam.Add(stats);
        }

        // "Enemy" 태그가 붙은 모든 오브젝트의 CharacterStats를 가져옴
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            CharacterStats stats = go.GetComponent<CharacterStats>();
            if (stats != null)
                enemyTeam.Add(stats);
        }

        currentTurnState = TurnState.Waiting;
        StartCoroutine(BattleLoop());
    }

    void DisablePlayerMovement()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = false;
        }
    }

    IEnumerator BattleLoop()
    {
        while (!IsBattleOver())
        {
            if (isPlayerTurn)
            {
                Debug.Log("플레이어 턴 시작");
                yield return StartCoroutine(PlayerTurn());
                isPlayerTurn = false;
            }
            else
            {
                Debug.Log("적 턴 시작");
                yield return StartCoroutine(EnemyTurn());
                isPlayerTurn = true;
            }

            // 각 팀의 상태 이상 효과 업데이트
            foreach (CharacterStats player in playerTeam)
                player.UpdateStatusEffects();
            foreach (CharacterStats enemy in enemyTeam)
                enemy.UpdateStatusEffects();
        }

        EndBattle();
    }

    IEnumerator PlayerTurn()
    {
        // 행동 선택 단계
        currentTurnState = TurnState.SelectingAction;
        selectedAction = ActionType.None;
        selectedSkillIndex = -1;
        Debug.Log("행동을 선택하세요 (G: 일반 공격, 키패드 1~8: 스킬, 키패드 9: 동료 영입)");

        while (currentTurnState == TurnState.SelectingAction)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                selectedAction = ActionType.NormalAttack;
                Debug.Log("일반 공격 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 0;
                Debug.Log("키패드 1번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 1;
                Debug.Log("키패드 2번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 2;
                Debug.Log("키패드 3번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 3;
                Debug.Log("키패드 4번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 4;
                Debug.Log("키패드 5번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 5;
                Debug.Log("키패드 6번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 6;
                Debug.Log("키패드 7번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                selectedAction = ActionType.Skill;
                selectedSkillIndex = 7;
                Debug.Log("키패드 8번 스킬 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                selectedAction = ActionType.Capture;
                Debug.Log("키패드 9번 선택. 동료 영입 액션 선택. 대상 선택 모드로 전환합니다.");
                break;
            }
            yield return null;
        }

        // 대상 선택 단계 시작: (예: 모든 적 체력바 숨기고 선택된 대상만 표시하는 UI 업데이트)
        currentTurnState = TurnState.SelectingTarget;
        selectedTargetIndex = 0;
        HideAllEnemyHealthBars();
        if (enemyTeam.Count > 0)
        {
            Debug.Log("대상 선택 모드 시작 (WS키로 이동, H키로 확정). 현재 대상: " + enemyTeam[selectedTargetIndex].characterName);
            UpdateSelectedEnemyUI(selectedTargetIndex);
        }
        else
        {
            Debug.LogWarning("공격할 적이 없습니다.");
            currentTurnState = TurnState.Waiting;
            yield break;
        }

        while (currentTurnState == TurnState.SelectingTarget)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                selectedTargetIndex = (selectedTargetIndex - 1 + enemyTeam.Count) % enemyTeam.Count;
                Debug.Log("대상 변경: " + enemyTeam[selectedTargetIndex].characterName);
                UpdateSelectedEnemyUI(selectedTargetIndex);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                selectedTargetIndex = (selectedTargetIndex + 1) % enemyTeam.Count;
                Debug.Log("대상 변경: " + enemyTeam[selectedTargetIndex].characterName);
                UpdateSelectedEnemyUI(selectedTargetIndex);
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                // 대상 선택 확정 및 행동 실행
                CharacterStats attacker = playerTeam[0];
                CharacterStats target = enemyTeam[selectedTargetIndex];

                if (selectedAction == ActionType.NormalAttack)
                {
                    int damage = attacker.GetPhysicalAttackPower() - target.GetDefense();
                    if (damage < 0) damage = 0;
                    if (target.GetResistance(ElementType.Physical) == ResistanceType.Weakness)
                    {
                        damage = (int)(damage * 1.5f);
                        Debug.Log("약점 공격! " + attacker.characterName + "의 일반 공격이 1.5배 데미지를 입힙니다.");
                    }
                    target.currentHP -= damage;
                    Debug.Log(attacker.characterName + "가 " + target.characterName + "에게 " + damage + "의 데미지를 입혔습니다.");
                }
                else if (selectedAction == ActionType.Skill)
                {
                    if (attacker.skills == null || attacker.skills.Count <= selectedSkillIndex)
                    {
                        Debug.LogWarning(attacker.characterName + "에게 해당 번호의 스킬이 없습니다.");
                        currentTurnState = TurnState.Waiting;
                        break;
                    }
                    GenericSkill skill = attacker.skills[selectedSkillIndex];
                    skill.Execute(attacker, target);
                    Debug.Log(attacker.characterName + "가 " + skill.skillName + " 스킬을 사용했습니다.");
                }
                else if (selectedAction == ActionType.Capture)
                {
                    // 동료 영입: 대상의 이름이 "Maria"여야 함
                    if (target.characterName.Equals("Maria", System.StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.Log(attacker.characterName + "가 " + target.characterName + "을(를) 동료로 영입합니다.");
                        // 새로운 동료 인스턴스를 생성 (동료용 프리팹 사용)
                        if (companionPrefab != null)
                        {
                            GameObject newCompanion = Instantiate(companionPrefab, companionSpawnPosition, Quaternion.identity);
// 새로운 동료를 파괴되지 않도록 설정
                            DontDestroyOnLoad(newCompanion);

                            CharacterStats companionStats = newCompanion.GetComponent<CharacterStats>();
                            if (companionStats != null)
                            {
                                // 적 오브젝트의 데이터를 새 동료에 복사
                                companionStats.characterName = target.characterName; // "Maria"
                                companionStats.level = target.level;
                                companionStats.baseHP = target.baseHP;
                                companionStats.currentHP = target.currentHP;
                                companionStats.baseMP = target.baseMP;
                                companionStats.currentMP = target.currentMP;
                                companionStats.strength = target.strength;
                                companionStats.magic = target.magic;
                                companionStats.stamina = target.stamina;
                                companionStats.speed = target.speed;
                                companionStats.physicalResistance = target.physicalResistance;
                                companionStats.fireResistance = target.fireResistance;
                                companionStats.iceResistance = target.iceResistance;
                                companionStats.windResistance = target.windResistance;
                                companionStats.lightningResistance = target.lightningResistance;
                                companionStats.darkResistance = target.darkResistance;
                                companionStats.lightResistance = target.lightResistance;
                                companionStats.skills = new List<GenericSkill>(target.skills);
                            }
                            // 후보 동료로 추가 (TeamManager에 이미 구현된 AddCandidateTeamMember 사용)
                            TeamManager.instance.AddCandidateTeamMember(newCompanion);
                        }
                        else
                        {
                            Debug.LogWarning("동료용 프리팹(companionPrefab)이 할당되어 있지 않습니다.");
                        }
                        // 캡처한 적 오브젝트 제거
                        enemyTeam.RemoveAt(selectedTargetIndex);
                        Destroy(target.gameObject);
                        Debug.Log(target.characterName + "이(가) 동료로 영입되었습니다.");

                        // 동료 영입 후 전투 종료
                        EndBattle();
                        yield break;
                    }
                    else
                    {
                        Debug.LogWarning("해당 몬스터는 동료로 영입할 수 없습니다: " + target.characterName);
                    }
                }

                if (selectedAction != ActionType.Capture)
                {
                    if (target.currentHP <= 0)
                    {
                        Debug.Log(target.characterName + "이(가) 쓰러졌습니다.");
                        enemyTeam.Remove(target);
                        Destroy(target.gameObject);
                    }
                }

                HideAllEnemyHealthBars();
                currentTurnState = TurnState.Waiting;
                break;
            }
            yield return null;
        }
    }

    IEnumerator EnemyTurn()
    {
        for (int i = enemyTeam.Count - 1; i >= 0; i--)
        {
            CharacterStats enemy = enemyTeam[i];
            if (enemy == null)
                continue;

            if (enemy.HasStatusEffect(StatusEffectType.Paralysis))
            {
                Debug.Log(enemy.characterName + "은(는) 마비 상태로 행동할 수 없습니다.");
                yield return new WaitForSeconds(1f);
                continue;
            }

            if (playerTeam.Count > 0)
            {
                int targetIndex = Random.Range(0, playerTeam.Count);
                CharacterStats target = playerTeam[targetIndex];

                float attackChoice = Random.value;
                if (attackChoice < 0.3f)
                {
                    // 일반 공격
                    int damage = enemy.GetPhysicalAttackPower() - target.GetDefense();
                    if (damage < 0) damage = 0;
                    if (target.GetResistance(ElementType.Physical) == ResistanceType.Weakness)
                    {
                        damage = (int)(damage * 1.5f);
                        Debug.Log("약점 공격! " + enemy.characterName + "의 일반 공격이 1.5배 데미지를 입힙니다.");
                    }
                    target.currentHP -= damage;
                    Debug.Log(enemy.characterName + "가 " + target.characterName + "에게 " + damage + "의 데미지를 입혔습니다.");
                }
                else
                {
                    // 스킬 공격 (적이 스킬이 있을 경우)
                    if (enemy.skills != null && enemy.skills.Count > 0)
                    {
                        int randomSkillIndex = Random.Range(0, enemy.skills.Count);
                        GenericSkill chosenSkill = enemy.skills[randomSkillIndex];
                        chosenSkill.Execute(enemy, target);
                        Debug.Log(enemy.characterName + "가 " + chosenSkill.skillName + " 스킬을 사용했습니다.");
                    }
                    else
                    {
                        // 스킬이 없으면 일반 공격으로 대체
                        int damage = enemy.GetPhysicalAttackPower() - target.GetDefense();
                        if (damage < 0) damage = 0;
                        if (target.GetResistance(ElementType.Physical) == ResistanceType.Weakness)
                        {
                            damage = (int)(damage * 1.5f);
                            Debug.Log("약점 공격! " + enemy.characterName + "의 일반 공격이 1.5배 데미지를 입힙니다.");
                        }
                        target.currentHP -= damage;
                        Debug.Log(enemy.characterName + "가 " + target.characterName + "에게 " + damage + "의 데미지를 입혔습니다.");
                    }
                }

                if (target.currentHP <= 0)
                {
                    Debug.Log(target.characterName + "이(가) 쓰러졌습니다.");
                    playerTeam.RemoveAt(targetIndex);
                    Destroy(target.gameObject);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    bool IsBattleOver()
    {
        return (playerTeam.Count == 0 || enemyTeam.Count == 0);
    }

    void EndBattle()
    {
        foreach (CharacterStats player in playerTeam)
        {
            if (player != null)
                player.ResetOverload();
        }
        foreach (CharacterStats enemy in enemyTeam)
        {
            if (enemy != null)
                enemy.ResetOverload();
        }

        if (playerTeam.Count > 0)
            Debug.Log("플레이어 팀 승리!");
        else
            Debug.Log("적 팀 승리!");

        SceneManager.LoadScene("MapScene");
    }

    // 선택된 적의 체력바만 활성화하고 나머지는 숨기는 함수
    void UpdateSelectedEnemyUI(int selectedIndex)
    {
        for (int i = 0; i < enemyTeam.Count; i++)
        {
            HealthBar hb = enemyTeam[i].GetComponentInChildren<HealthBar>(true);
            if (hb != null)
            {
                hb.gameObject.SetActive(i == selectedIndex);
                Debug.Log("Enemy " + i + " HealthBar active: " + (i == selectedIndex));
            }
            else
            {
                Debug.LogWarning("Enemy " + i + " does not have a HealthBar in its children!");
            }
        }
    }

    // 모든 적의 체력바를 숨기는 함수
    void HideAllEnemyHealthBars()
    {
        for (int i = 0; i < enemyTeam.Count; i++)
        {
            HealthBar hb = enemyTeam[i].GetComponentInChildren<HealthBar>(true);
            if (hb != null)
            {
                hb.gameObject.SetActive(false);
            }
        }
    }
}
