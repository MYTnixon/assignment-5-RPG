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

    public float speed;
    
    private bool facingRight;
    private bool isWalking;

    public float knockback;
    public float knockTime;

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public Animator attackAnim;

    public bool armourIsEquipped;
    public bool weaponIsEquipped;

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
                facingRight = false;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Input.GetKey("d"))
            {
                animator.Play("Player_Run");
                facingRight = true;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
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

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6f, 6f), Mathf.Clamp(transform.position.y, -4.4f, 0.3f), transform.position.z);
    }

    void Move()
    {
        rbody.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }

    public void Damage(int dmg)
    {
        if (armourIsEquipped)
        {
            GameManager.Instance.TakeDamage(1);
        }
        else
        {
            GameManager.Instance.TakeDamage(2);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NextLevel"))
        {
            int c = SceneManager.GetActiveScene().buildIndex;
            if (c < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(c + 1);
        }
    }
}
