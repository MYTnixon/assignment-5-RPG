using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Vector3 vector;

    public float speed;
    public int hp;
    public float knockback;
    public float knockTime;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6f, 6f), Mathf.Clamp(transform.position.y, -4.4f, 0.3f), transform.position.z);
    }

    void Move()
    {
        rbody.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null)
            {
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * knockback;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                enemy.isKinematic = true;
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
}
