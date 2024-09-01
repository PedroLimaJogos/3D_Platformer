using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
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
}
