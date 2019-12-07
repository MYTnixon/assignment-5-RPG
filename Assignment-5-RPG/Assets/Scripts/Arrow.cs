using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifeTime;
    public float speed;

    private void Start()
    {
        Invoke("DestroyArrow", lifeTime);
    }

    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
    }

    void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
