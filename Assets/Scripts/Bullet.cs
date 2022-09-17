using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 1;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddRelativeForce(new Vector2(0, speed), ForceMode2D.Impulse);

        Destroy(gameObject, 30f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Meteor meteor = collider.gameObject.GetComponent<Meteor>();
        if (meteor)
        {
            meteor.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
