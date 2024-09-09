using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        

        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);  // Keep it alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


public class checkpointManager : Singleton<checkpointManager>
{
    public int lastCheckpointKey = 0;
    public List<CheckpointBase> checkpoints;

    // Check if the player has reached any checkpoint
    public bool hasCheckpoint()
    {
        if(lastCheckpointKey > 0)
        {
            return true;
        }
        else return false;
        
    }

    protected override void Awake() {
        int dados = CheckPointData.Instance.retornaIndex();
        if(dados > 0)
        {
            lastCheckpointKey = dados;
            Debug.Log(lastCheckpointKey);

            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.key <= dados)
                {
                    checkpoint.TurnItOn(); // Ativa o checkpoint (visual ou funcionalmente)
                }
            }
        }
    }

    // Save the latest checkpoint
    public void SaveCheckPoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
    }

    // Get the position of the last checkpoint reached
    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

    public void LoadCheckpoint(int lastCheckpoint)
    {
        lastCheckpointKey = lastCheckpoint;
    }
}
