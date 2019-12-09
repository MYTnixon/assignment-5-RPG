using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public int health;
    public int attack;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    public float knockback;
    public float knockTime;

    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    private Rigidbody2D rBody;
    private Vector3 moveDirection;
    SpriteRenderer spriteRenderer;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = GameObject.FindWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (rBody.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rBody.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }

        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            rBody.velocity = moveDirection;
            if(timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            rBody.velocity = Vector2.zero;
            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);
                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11f, 46f), Mathf.Clamp(transform.position.y, -5f, -0.6f), transform.position.z);
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerController.weaponIsEquipped)
        {
            health -= 2;
        }
        else
        {
            health -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D player = other.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                player.isKinematic = false;
                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * knockback;
                player.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(player));
            }
            playerController.Damage(1);
        }
        else if (other.gameObject.CompareTag("Arrow"))
        {
            health -= 2;
        }
        else if (other.gameObject.CompareTag("Sword"))
        {
            health -= 1;
        }
    }

    private IEnumerator KnockCo(Rigidbody2D player)
    {
        if (player != null)
        {
            yield return new WaitForSeconds(knockTime);
            player.velocity = Vector2.zero;
            player.isKinematic = true;
        }
    }
}
