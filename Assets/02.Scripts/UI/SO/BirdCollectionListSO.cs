using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BirdCollectionListSO", menuName = "UISO/BirdCollectionListSO")]
public class BirdCollectionListSO : ScriptableObject
{
    public List<CollectionSO> list;
}
