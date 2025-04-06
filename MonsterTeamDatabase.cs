using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterTeamDatabase", menuName = "Monster/Team Database")]
public class MonsterTeamDatabase : ScriptableObject {
    public List<MonsterTeamDefinition> teamDefinitions;
}
