using UnityEngine;
using UnityEngine.UI;

public class BirdCollectionUI : AbstractUI
{
    [SerializeField] private BirdCollectionListSO birdListSO;
    [SerializeField] private CollectionSetter collectionPrefab;

    public override void OnUIOpen()
    {
        base.OnUIOpen();

        for (int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        RaycastToggle ray = GetComponent<RaycastToggle>();
        
        foreach (CollectionSO collectionSO in birdListSO.list)
        {
            CollectionSetter collection = Instantiate(collectionPrefab, transform);
            collection.SetCollection(collectionSO);
        }
    }

    public override void OnUIClose()
    {
        base.OnUIClose();
        UIManager.Instance.DoSomething = false;
    }
}
