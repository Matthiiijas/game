using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    //Attack
    Transform Weapon;
    public float attackRange;
    public float attackDistance;
    public float knockbackStrength;
    public LayerMask hittableLayers;
    public float attackCoolDown, attackCoolDownTimer;
    public Vector3 attackOffset;
    public float collectEnemiesTime;
    float collectEnemiesTimer;
    List<GameObject> collectedEntities = new List<GameObject>();
    bool attacking;
    //public bool swordFront;

    //Shoot
    bool shooting;

    //Move
    InputMaster controls;
    Vector2 inputMove, realMove, remainMove = new Vector2(0,-1);
    public Vector2 transitionDir;
    public float speed;

    public bool transitioning = false, canMove = true;

    //Inventory
    public int moneyCount;
    public GameObject coinCount;

    void Awake () {
        //Define Components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //Get Transform of Weapon
        Weapon = transform.Find("Weapon");
        //Retrieve inputs
        controls = new InputMaster();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Attack.started += ctx => Attack();
        controls.Player.Shoot.started += ctx => shooting = true;
        controls.Player.Shoot.canceled += ctx => shooting = false;
        //Setup Attack Cooldown Timer
        attackCoolDownTimer = attackCoolDown;
    }

    void Update() {
        //if(swordFront) Weapon.GetComponent<SpriteRenderer>().sortingOrder = sr.sortingOrder + 5;
        //else Weapon.GetComponent<SpriteRenderer>().sortingOrder = sr.sortingOrder - 5;

        //Attack Cooldown Timer
        if(attackCoolDownTimer > 0) attackCoolDownTimer -= Time.deltaTime;

        //Play Idle and Movement animations
        Animate();

        coinCount.GetComponent<TextMeshProUGUI>().SetText(moneyCount.ToString());
    }

    void FixedUpdate () {

        //Movement
        if(canMove) rb.AddForce(inputMove * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        if(shooting) Aim();
        else Shoot();

    }

    void Animate() {
        //Define last direction
        if(inputMove != Vector2.zero) {
            if(Mathf.Abs(inputMove.x) < Mathf.Abs(inputMove.y)) remainMove = new Vector2(0, inputMove.y);
            else  remainMove = new Vector2(inputMove.x, 0);
            remainMove.Normalize();
        }
        //Set Values for Animator
        anim.SetFloat("MoveX", rb.velocity.x);
        anim.SetFloat("MoveY", rb.velocity.y);
        anim.SetFloat("remainX", remainMove.x);
        anim.SetFloat("remainY", remainMove.y);
        anim.SetFloat("Velocity", inputMove.magnitude);
    }

    void Attack() {
        //If Cooldown is over...
        if(attackCoolDownTimer <= 0) {
            //Play attack animation
            Weapon.position = transform.position + attackOffset + (Vector3) remainMove.normalized * attackDistance;
            //Collect all enemies in attackRange and -Distance for collect time...
            collectEnemiesTimer = collectEnemiesTime;
            while(collectEnemiesTimer > 0) {
                //Set position of Weapon
                foreach(Collider2D entity in Physics2D.OverlapCircleAll(Weapon.position, attackRange, hittableLayers)) {
                    collectedEntities.Add(entity.gameObject);
                }
                collectEnemiesTimer -= Time.deltaTime;
            }
            foreach(GameObject entity in collectedEntities) {
                if(entity.CompareTag("Enemy")) entity.GetComponent<DamageManager>().TakeDamage(1,remainMove,knockbackStrength);
                else entity.GetComponent<Rigidbody2D>().AddForce(remainMove * knockbackStrength);
            }
            anim.SetTrigger("Attack");
            collectedEntities.Clear();
            //Reset Cooldown Timer
            attackCoolDownTimer = attackCoolDown;
        }
    }

    void Aim() {
        canMove = false;
    }

    void Shoot() {
        canMove = true;
    }

    //Draw attackRange circle
    void OnDrawGizmosSelected() {
        Weapon = transform.Find("Weapon");
        Gizmos.DrawWireSphere(Weapon.position, attackRange);
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
