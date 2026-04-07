using UnityEngine;

[RequireComponent(typeof(RaycastToggle))]
public abstract class AbstractUI : MonoBehaviour
{
    [field: SerializeField] public UICategory MyCategory { get; private set; }
    private RaycastToggle toggle;

    protected virtual void Awake()
    {
        toggle = GetComponentInChildren<RaycastToggle>();
    }

    public virtual void OnUIOpen()
    {
        gameObject.SetActive(true); 
        toggle.ChangeAllRaycastTargets(true);
    }
    public virtual void OnUIClose()
    {
        toggle.ChangeAllRaycastTargets(false);
        gameObject.SetActive(false);
    }
}
