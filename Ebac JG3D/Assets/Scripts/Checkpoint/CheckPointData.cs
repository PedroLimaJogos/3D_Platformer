using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointData : Singleton<CheckPointData>
{
    public int checkpointIndex;

    protected override void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public int retornaIndex()
    {
        return checkpointIndex;
    }

    public void salvaIndex(int numero)
    {
        checkpointIndex = numero;
    }
}
