using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDestroy = 2f;
    public float speed = 50f;

    private void Awake() {
        Destroy(gameObject, timeToDestroy);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);        
    }

    private void OnCollisionEnter(Collision collision) {
        
    }
}
