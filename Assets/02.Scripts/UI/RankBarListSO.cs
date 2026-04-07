using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RankBarListSO", menuName = "UISO/RankBarListSO")]
public class RankBarListSO : ScriptableObject
{
    public List<RankBarSO> list;
}
