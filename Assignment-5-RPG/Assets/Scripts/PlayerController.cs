using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Vector3 vector;
    public GameObject bowWeapon;
    public GameObject swordWeapon;

    public float speed;
    public int hp;

    private bool isWalking;

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public Animator attackAnim;

    public bool armourIsEquipped;
    public bool weaponIsEquipped;
    public bool bowIsEquipped;
    public bool swordIsEquipped;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
                }
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
        vector = Vector3.zero;
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");
        if (vector != Vector3.zero)
        {
            Move();
            if (Input.GetKey("a"))
            {
                animator.Play("Player_Run");
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKey("d"))
            {
                animator.Play("Player_Run");
                spriteRenderer.flipX = false;
            }
            if (Input.GetKey("w"))
            {
                animator.Play("Player_Run");
            }
            else if (Input.GetKey("s"))
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            animator.Play("Player_Idle");
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11f, 65f), Mathf.Clamp(transform.position.y, -5f, -0.6f), transform.position.z);

        if (bowIsEquipped)
        {
            bowWeapon.SetActive(true);
        }
        else
        {
            bowWeapon.SetActive(false);
        }

        if (swordIsEquipped)
        {
            swordWeapon.SetActive(true);
        }
        else
        {
            swordWeapon.SetActive(false);
        }
    }

    void Move()
    {
        rbody.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }

    public void Damage(int dmg)
    {
        if (armourIsEquipped)
        {
            hp -= 1;
        }
        else
        {
            hp -= 2;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        hp = data.playerHealth;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
    }
}
