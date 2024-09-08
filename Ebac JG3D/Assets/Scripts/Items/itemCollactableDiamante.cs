using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class itemCollactableDiamante : itemCollactableBase
{
    // Start is called before the first frame update
    protected override void OnCollect()
    {
        Debug.Log("diamante coletado");
        base.OnCollect();
        // itemManager.Instance.AddCoins(5);
    }
}
