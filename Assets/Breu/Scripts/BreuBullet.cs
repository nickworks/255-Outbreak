using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreuBullet : MonoBehaviour
{
    public float speed = 20;

    public float lifeSpan = 3;


    private float age = 0;

    Vector3 velocity = Vector3.zero;

    void Start()
    {
        velocity = transform.right;
    }


    void Update()
    {
        transform.position += velocity * speed * Time.deltaTime;
        age += Time.deltaTime;
        if (age >= lifeSpan)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.CompareTag("BreuBoss"))
        {
            Debug.Log("Boss hit!");
        }
    }


}
