using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class Walk : EnemyBase {
        public GameObject[] waypoints;
        public float minDistance = 1f;
        public float speed = 1f;

        private int _index = 0;


        public  override void Update() {
            base.Update();
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
}
