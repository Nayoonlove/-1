using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager instance;

    // 후보 팀원 (최대 20명)
    public List<GameObject> candidateTeamMembers;
    public int maxCandidateCount = 20;

    // 선발 팀원 (최대 4명)
    public List<GameObject> selectedTeamMembers;
    public int maxSelectedCount = 4;

    void Awake()
    {
        // 싱글톤 패턴 적용 및 이 오브젝트를 씬 전환 시 유지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // 리스트가 null인 경우 초기화
            if(candidateTeamMembers == null)
                candidateTeamMembers = new List<GameObject>();
            if(selectedTeamMembers == null)
                selectedTeamMembers = new List<GameObject>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddCandidateTeamMember(GameObject candidate)
    {
        if (candidateTeamMembers.Count < maxCandidateCount)
        {
            candidateTeamMembers.Add(candidate);
            Debug.Log(candidate.name + " 후보 팀에 추가되었습니다. 현재 후보 수: " + candidateTeamMembers.Count);
            return true;
        }
        else
        {
            Debug.Log("후보 팀이 꽉 찼습니다. " + candidate.name + " 추가 불가.");
            return false;
        }
    }

    public bool RemoveCandidateTeamMember(GameObject candidate)
    {
        if (candidateTeamMembers.Contains(candidate))
        {
            candidateTeamMembers.Remove(candidate);
            Debug.Log(candidate.name + " 후보 팀에서 제거되었습니다. 현재 후보 수: " + candidateTeamMembers.Count);
            return true;
        }
        else
        {
            Debug.Log(candidate.name + "은(는) 후보 팀에 없습니다.");
            return false;
        }
    }

    public bool PromoteCandidateToSelected(GameObject candidate)
    {
        if (selectedTeamMembers.Count >= maxSelectedCount)
        {
            Debug.Log("선발 팀이 꽉 찼습니다. " + candidate.name + " 승격 불가.");
            return false;
        }
        if (candidateTeamMembers.Contains(candidate))
        {
            candidateTeamMembers.Remove(candidate);
            selectedTeamMembers.Add(candidate);
            Debug.Log(candidate.name + "이(가) 선발 팀으로 승격되었습니다. 현재 선발 팀원 수: " + selectedTeamMembers.Count);
            return true;
        }
        else
        {
            Debug.Log(candidate.name + "은(는) 후보 팀에 없습니다.");
            return false;
        }
    }

    public bool DemoteSelectedTeamMember(GameObject member)
    {
        if (selectedTeamMembers.Contains(member))
        {
            if (candidateTeamMembers.Count >= maxCandidateCount)
            {
                Debug.Log("후보 팀이 꽉 찼습니다. " + member.name + " 강등 불가.");
                return false;
            }
            selectedTeamMembers.Remove(member);
            candidateTeamMembers.Add(member);
            Debug.Log(member.name + "이(가) 후보 팀으로 강등되었습니다. 현재 선발 팀원 수: " + selectedTeamMembers.Count);
            return true;
        }
        else
        {
            Debug.Log(member.name + "은(는) 선발 팀에 없습니다.");
            return false;
        }
    }

    public List<GameObject> GetCandidateTeamMembers()
    {
        return new List<GameObject>(candidateTeamMembers);
    }

    public List<GameObject> GetSelectedTeamMembers()
    {
        return new List<GameObject>(selectedTeamMembers);
    }
}
