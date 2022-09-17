using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public int damage = 1;
    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 30f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            FindObjectOfType<ScoreManager>().AddPoints(1);
            Destroy(gameObject);
        }
    }
}
