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

    //Dash
    public float dashSpeed;
    public AudioClip dashClip;
    bool dashing;
    public float dashTime;
    public float dashKnockbackStrength;

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
        controls.Player.Ability.canceled += ctx => StartCoroutine(Dash());
        //Setup Attack Cooldown Timer
        attackCoolDownTimer = attackCoolDown;
    }

    void Update() {
        //Attack Cooldown Timer
        if(attackCoolDownTimer > 0) attackCoolDownTimer -= Time.deltaTime;

        //Play Idle and Movement animations
        Animate();

        if(coinCount != null) coinCount.GetComponent<TextMeshProUGUI>().SetText(moneyCount.ToString());
    }

    void FixedUpdate () {
        if(Input.GetKey("u")) {
            GetComponent<HealthController>().maxHealthPoints = 20;
            GetComponent<HealthController>().healthPoints = 20;
            GetComponent<ManaController>().maxManaPoints = 20;
            GetComponent<ManaController>().manaPoints = 20;
        }

        //Movement
        if(canMove) rb.AddForce(inputMove.normalized * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        if(inputMove != Vector2.zero) remainMove = inputMove.normalized;
    }

    void Animate() {
        if(inputMove != Vector2.zero) remainMove = inputMove;
        //Set Values for Animator
        anim.SetFloat("MoveX", Cardinalize(rb.velocity).x);
        anim.SetFloat("MoveY", Cardinalize(rb.velocity).y);
        anim.SetFloat("remainX", Cardinalize(remainMove).x);
        anim.SetFloat("remainY", Cardinalize(remainMove).y);
        anim.SetFloat("Velocity", rb.velocity.sqrMagnitude);
        //test
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
                if(entity.CompareTag("Enemy")) entity.GetComponent<HealthController>().TakeDamage(1,remainMove,knockbackStrength);
                else entity.GetComponent<Rigidbody2D>().AddForce(remainMove * knockbackStrength);
            }
            anim.SetTrigger("Attack");
            collectedEntities.Clear();
            //Reset Cooldown Timer
            attackCoolDownTimer = attackCoolDown;
        }
    }

    IEnumerator Dash() {
        if(GetComponent<ManaController>().manaPoints > 0) {
            dashing = true;
            GetComponent<HealthController>().invulnerable = true;
            AudioSource.PlayClipAtPoint(dashClip, transform.position, 1);
            rb.AddForce(remainMove * dashSpeed, ForceMode2D.Impulse);
            GetComponent<ManaController>().ModifyMana(-1);
            yield return new WaitForSeconds(dashTime);
            dashing = false;
            GetComponent<HealthController>().invulnerable = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(dashing && col.gameObject.CompareTag("Enemy")) {
            col.gameObject.GetComponent<HealthController>().TakeDamage(1, remainMove, dashKnockbackStrength);
        }
    }

    //Draw attackRange circle
    void OnDrawGizmosSelected() {
        Weapon = transform.Find("Weapon");
        Gizmos.DrawWireSphere(Weapon.position, attackRange);
    }

    Vector2 Cardinalize(Vector2 vector) {
        if(vector != Vector2.zero) {
            if(Mathf.Abs(vector.x) < Mathf.Abs(vector.y)) vector = new Vector2(0, vector.y);
            else vector = new Vector2(vector.x, 0);
        }
        return vector.normalized;
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
