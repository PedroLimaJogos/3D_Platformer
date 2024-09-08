using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;
public class ActionLifePack : MonoBehaviour
{
    public SOint soInt;
    public KeyCode keycode = KeyCode.L;
    private void Start() {
        soInt = itemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            itemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();
        }
    }

    private void Update() {
        if(Input.GetKeyDown(keycode))
        {
            RecoverLife();
        }
    }
}
