using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDestroy = 2f;
    public float speed = 50f;
    public float damageAmount = 10;

    public List<string> tagsToHit;

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

        foreach(var t in tagsToHit) {
            if(collision.transform.tag == t) {
                var damageable = collision.transform.GetComponent<IDamageable>();

                if(damageable != null) 
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(damageAmount,dir);
                    
                }
                break;
            }
        }
        if(collision.transform.tag != "bullet")
            Destroy(gameObject);
    }
}
