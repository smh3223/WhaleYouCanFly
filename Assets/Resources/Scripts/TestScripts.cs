using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private Vector3 lastVelocity;
    private Vector3 lastContact;

    private Ball ball;
    public CSVReader reader;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        ball = GetComponent<Ball>();
    }
    
    void Update()
    {
        if ((rb.velocity.x == 0 || rb.velocity.y == 0) && ball.getIsShooting())
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, lastContact);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
        lastVelocity = rb.velocity;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("star"))
        {

            if (spr.color == collision.GetComponent<SpriteRenderer>().color)
            {
                Destroy(collision.gameObject);
                reader.EatingStar();

                if(reader.GetStarCount() == 0)
                {
                    ball.Reset();
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        lastContact = collision.contacts[0].normal;

        rb.velocity = direction * Mathf.Max(speed, 0f);
        spr.color = collision.gameObject.GetComponent<SpriteRenderer>().color;



        if (collision.collider.CompareTag("land"))
        {
            ball.Reset();
            ball.setIsShooting(false);
            if (ball.opportunity == 0)
            {
                ball.ONReBtn();
            }
        }

        

    }

}
