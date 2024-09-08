using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Itens;
public class ChestItemCoins : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;

    public Vector2 RandomRange = new Vector2(-2f,2f);

    public float tweenEndTime = .5f;

    private List<GameObject> _itens = new List<GameObject>();

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItens();
    }

    [NaughtyAttributes.Button]
    private void CreateItens()
    {
        for(int i = 0; i< coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(RandomRange.x,RandomRange.y) + Vector3.right * Random.Range(RandomRange.x,RandomRange.y);
            item.transform.DOScale(0,1f).SetEase(Ease.OutBack).From();
            _itens.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach(var i in _itens)
        {
            i.transform.DOMoveY(2f,tweenEndTime).SetRelative();
            i.transform.DOScale(0,tweenEndTime/2).SetDelay(tweenEndTime/2);
            itemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
