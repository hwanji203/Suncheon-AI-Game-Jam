using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CutSceneListSO", menuName = "UISO/CutSceneListSO")]
public class CutSceneListSO : ScriptableObject
{
    public List<CutsceneElement> list;
}
