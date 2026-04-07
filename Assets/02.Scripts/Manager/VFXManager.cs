using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum EVFXType
{
    MudSkipper = 0,
    Eagle = 1
}

public class VFXManager : MonoSingleton<VFXManager>
{
    [SerializeField] SerializedDictionary<EVFXType, GameObject> vfxDictionary = new SerializedDictionary<EVFXType, GameObject>();
    
    public void ShowVFX(EVFXType vfxType, Vector3 position)
    {
        Instantiate(vfxDictionary[vfxType].gameObject, position, Quaternion.identity);
    }
}
