using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastToggle : MonoBehaviour
{
    [SerializeField] private List<Graphic> graphics;

    [ContextMenu("Disable RaycastTargets")]
    public void ChangeAllRaycastTargets(bool value)
    {
        foreach (Graphic g in graphics)
        {
            g.raycastTarget = value;
        }
    }
}