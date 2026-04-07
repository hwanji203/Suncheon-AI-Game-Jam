using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveValue(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public void SaveValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public void SaveValue(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}
