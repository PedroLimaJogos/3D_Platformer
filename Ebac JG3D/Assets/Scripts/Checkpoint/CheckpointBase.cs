using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    [Header("Sounds")]
        public SFXType sFXType;

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
    public void TurnItOn()
    {
        SFXPool.Instance.Play(sFXType);
        meshRenderer.material.SetColor("_EmissionColor",Color.green);
        SaveManager.Instance.ChangeCheckpoint(key);

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
