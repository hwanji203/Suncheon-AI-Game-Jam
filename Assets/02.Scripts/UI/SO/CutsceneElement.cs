using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneElement", menuName = "UISO/CutsceneElement")]
public class CutsceneElement : ScriptableObject
{
    public Sprite image;       
    public string text;        
    public float duration = 2f;
}
