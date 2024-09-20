using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class Walk : EnemyBase {
      public GameObject[] waypoints;
    public float minDistance = 1f;
    public float speed = 1f;
    public float followSpeed = 2f;  // Velocidade ao seguir o jogador

    private int _index = 0;
    public Transform player;  // Referência ao jogador
    private bool isFollowing = false;  // Indica se o inimigo está seguindo o jogador

    public SphereCollider detectionArea;  // Referência à área de detecção

    private void Start() {
        if (detectionArea != null) {
            detectionArea.isTrigger = true;  // Certifica-se de que o collider é um trigger
        }
    }

    public override void Update() {
        base.Update();

        if (isFollowing && player != null) {
            // Segue o jogador
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * followSpeed);
        } else {
            // Verifica se está próximo o suficiente do waypoint atual
            if (Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance) {
                _index++;
                if (_index >= waypoints.Length) {
                    _index = 0;
                }
            }

            // Move o objeto em direção ao waypoint atual
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, Time.deltaTime * speed);
            transform.LookAt(waypoints[_index].transform.position);
        }
    }

    private void OnTriggerEnter(Collider detectionArea) {
        if (detectionArea.CompareTag("Player")) {  // Verifica se o objeto é o jogador
        Debug.Log("Achei o player");
            isFollowing = true;  // Ativa o seguimento
            transform.LookAt(player.transform.position);
        }
    }

    private void OnTriggerExit(Collider detectionArea) {
        if (detectionArea.CompareTag("Player")) {  // Verifica se o objeto é o jogador
            Debug.Log("Perdi o player");
            isFollowing = false;  // Desativa o seguimento
            base.lookAtPlayer = false;
        }
    }
}
}