using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActive = false;
    private string checkpointKey = "CheckpointKey";
    private void OnTriggerEnter(Collider other) {
        if(!checkpointActive && other.transform.tag == "Player") {
            CheckCheckpoint();
        }
        
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        saveCheckpoint();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor",Color.green);

    }
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor",Color.red);
    }

    private void saveCheckpoint()
    {
        // if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
        //     PlayerPrefs.SetInt(checkpointKey,key);

        checkpointManager.Instance.SaveCheckPoint(key);

        checkpointActive = true;
    }
}
