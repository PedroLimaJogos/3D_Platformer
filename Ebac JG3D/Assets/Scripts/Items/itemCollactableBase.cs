using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Adicione esta linha para usar DOTween


namespace Itens{
    public class itemCollactableBase : MonoBehaviour
    {
        public string tagCompare = "Player";
        public ParticleSystem particleSystem;
        public Collider collider;
        public float timeToHide = 3f;
        public GameObject graphicItem;

        // Configurações de Tween
        public float floatAmount = 0.5f; // Quantidade de flutuação para cima e para baixo
        public float floatDuration = 2f; // Duração do ciclo de flutuação
        public float rotationSpeed = 30f; // Velocidade de rotação em graus por segundo

        [Header("Sounds")]
        public SFXType sFXType;
        public ItemType itemType;


        private void Awake()
        {
            //if (particleSystem != null) particleSystem.transform.SetParent(null);
        }
        private void Start()
        {
            // Inicializa os Tweenings ao iniciar
            StartFloating();
            StartRotating();
        }

        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log("Tocou em algo");
            if (collision.transform.CompareTag(tagCompare))
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sFXType);
        }

        protected virtual void Collect()
        {
            PlaySFX();
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
            // if (audioSource != null) audioSource.Play();
            itemManager.Instance.AddByType(itemType);   
        }

        private void Update()
        {

        }

        private void StartFloating()
        {
            if (graphicItem != null)
            {
                // Flutuar para cima e para baixo
                graphicItem.transform.DOMoveY(graphicItem.transform.position.y + floatAmount, floatDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo); // Loop infinito com efeito Yoyo
            }
        }

        private void StartRotating()
        {
            if (graphicItem != null)
            {
                // Rotacionar lentamente
                graphicItem.transform.DORotate(new Vector3(0, 360, 0), rotationSpeed, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1); // Loop infinito
            }
        }

    }
}