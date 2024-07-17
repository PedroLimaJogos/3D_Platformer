using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDestroy = 2f;
    public float speed = 50f;
    public float damageAmount = 0;

    private void Awake() {
        Destroy(gameObject, timeToDestroy);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);        
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision);
        var damageable = collision.transform.GetComponent<IDamageable>();

        if(damageable != null) 
        {
            damageable.Damage(damageAmount);
            Destroy(gameObject);
        }  
    }
}
