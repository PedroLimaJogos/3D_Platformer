using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cloth;
public class Player : Singleton<Player>//, IDamageable
{
    public CharacterController characterController;
     public Animator animator;

     private bool _alive = true;

    [SerializeField]private ClothChanger _clothChanger;

    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public List <Collider> colliders;


    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private float vSpeed = 0;
    public List<UIFillUpdate> uiGunUpdaters;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Flash")]
    public HealthBase healthBase;

    private void OnValidate() {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Start() {
        OnValidate();
        GetLifeUI();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }
    private void OnKill(HealthBase h)
    {
        if(_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");  
            colliders.ForEach(i => i.enabled = false); 

            Invoke(nameof(Revive),3f);
        }
    }

    private void GetLifeUI()
    {
        uiGunUpdaters = GameObject.FindObjectsOfType<UIFillUpdate>()
        .Where(u => u.gameObject.name == "LifeSquare")
        .ToList();
    }
    public float timeToRecharge = 1f;
    IEnumerator rechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            uiGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
    }

    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        Debug.Log("treme");
        ShakeCamera.Instance.Shake();
    }

    private void Revive()
    {
        animator.SetTrigger("Revive");
        Respawn();
        _alive = true;
        colliders.ForEach(i => i.enabled = true); 
        healthBase.ResetLife();
        StartCoroutine(rechargeCoroutine());
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }
    #endregion

    private void Update() {
    
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;


        if(characterController.isGrounded){
            vSpeed = 0;
            if(Input.GetKeyDown(KeyCode.Space)){
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;



        var isWalking = inputAxisVertical != 0;
        if(isWalking){
            if(Input.GetKey(keyRun)){
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else{
                animator.speed = 1;
            }
        }


        //Corre animação
        characterController.Move(speedVector * Time.deltaTime);
        if(inputAxisVertical !=0)
            {             
                animator.SetBool("run", true);       
            }
        else
            {
                animator.SetBool("run", false);
            }
    
        

    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(checkpointManager.Instance.hasCheckpoint())
        {
            transform.position = checkpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed,duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup,duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }
}
