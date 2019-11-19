using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public float lifespan = 3;

    float age = 0;

    Vector3 velocity = Vector3.zero;

    void Start()
    {
        velocity = transform.right * speed;
    }

    void Update()
    {
        age += Time.deltaTime;
        if (age >= lifespan) Destroy(gameObject);

        transform.position += velocity * Time.deltaTime;
    }
}
