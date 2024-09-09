using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens{
    public class itemCollactableBase : MonoBehaviour
    {
        // Start is called before the first frame update
        public ItemType itemType;
        public string tagCompare = "Player";
        public ParticleSystem particleSystem;
        public Collider collider;
        public float timeToHide = 3f;
        public GameObject graphicItem;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystem != null) particleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log("Tocou em algo");
            if (collision.transform.CompareTag(tagCompare))
            {
                if(collider != null) collider.enabled = false;
                Collect();
            }
        }
        protected virtual void Collect()
        {
            if(collider != null) collider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke(nameof(HideObject), timeToHide);
            OnCollect();
        }

        private void HideObject()
        {

            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particleSystem != null) particleSystem.Play();
            if (audioSource != null) audioSource.Play();
            itemManager.Instance.AddByType(itemType);   
        }

        private void Update()
        {

        }

    }
}