using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using Boss;
using Unity.VisualScripting;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private AnimationBase _animationBase;
        public Collider collider;
        public ParticleSystem particleSystem;
        public FlashColor flashColor;
        public float startLife = 10;
        public bool lookAtPlayer = false;

        public BossBase bossBase;

        
        [SerializeField] private float _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase =  Ease.OutBack;
        public bool startWithBornAnimation = true;
        

        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start() {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            BornAnimation();
        }
        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if(collider != null) collider.enabled = false;
            Destroy(gameObject,3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            if(bossBase)
            {
                bossBase.StartBoss();
            }
        }

        public void OnDamage(float f)
        {
            if(flashColor != null) flashColor.Flash();
            if(particleSystem != null) particleSystem.Emit(15);

            transform.position -= transform.forward;

            _currentLife -= f;

            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region Animation

        private void BornAnimation()
        {
            transform.DOScale(0,startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion


        // Debug
        

        public void Damage(float damage)
        {
            Debug.Log(damage);
            OnDamage(damage);
        }
        public void Damage(float damage,Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision) {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null) 
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update(){
        if(lookAtPlayer){
        
            transform.LookAt(_player.transform.position);
            }
        }
    }
}
