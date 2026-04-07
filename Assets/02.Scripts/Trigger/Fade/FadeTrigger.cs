using System.Collections;
using UnityEngine;

public class FadeTrigger : TriggerBase
{
    [SerializeField] private float fadeDelay = 1.0f;
    protected void Awake()
    {
        base.Awake();
    }
    
    protected void Start()
    {
        base.Start();
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        
        base.OnTriggerEnter(other);

        StartCoroutine(FadeWithDelay());
    }

    private IEnumerator FadeWithDelay()
    {
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(fadeDelay);
        FadeManager.Instance.FadeIn();
    }
}
