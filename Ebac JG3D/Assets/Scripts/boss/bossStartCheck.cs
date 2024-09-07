using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossStartCheck : MonoBehaviour
{
    public string tagToCheck = "Player";

    public GameObject bossCamera;
    public Color gizmoColor = Color.yellow;

    private void Awake() {
        bossCamera.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Entrou");
        if(other.transform.tag == tagToCheck)
        {
            TurnCameraOn();
            Debug.Log("liga");
        }
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos() {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position,transform.localScale.y);
    }
}
