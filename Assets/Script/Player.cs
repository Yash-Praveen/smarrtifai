using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int speed,rotSpeed,jumpForce;

    Rigidbody2D rb;
    CircleCollider2D col;
    Transform ball;
    SpriteRenderer ballImage;

    bool onGound,doubleJump;
    int distance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        ball = transform.GetChild(0);
        ballImage = ball.GetComponent<SpriteRenderer>();
        distance = Convert.ToInt32(transform.position.x);
    }
    void Update()
    {
        if ((onGound||doubleJump) && Input.GetMouseButtonDown(0))
        {
            if (onGound)
                onGound = false;
            else
                doubleJump = false;
            rb.AddForce(Vector2.up * jumpForce);
        }
        if (onGound)
            rb.velocity=Vector2.right * Time.deltaTime * speed;
        ball.Rotate(Vector3.back* Time.deltaTime*rotSpeed);

        if (distance < Convert.ToInt32(transform.position.x))
        {
            GameManager.instense.Score();
            distance = Convert.ToInt32(transform.position.x);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Platform":
                onGound = true;
                doubleJump = true;
                break;
            case "Rock":
                GameManager.instense.RockHit();
                StartCoroutine(DisEnCol());
                break;
        }
    }

    IEnumerator DisEnCol()
    {
        col.enabled = false;
        rb.gravityScale = 0;
        for(int i = 0; i < 10; i++)
        {
            if(i%2==0)
                BallAlphaSet(0.5f);
            else
                BallAlphaSet(1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        BallAlphaSet(1);
        col.enabled = true;
        rb.gravityScale = 1;
    }

    void BallAlphaSet(float value)
    {
        Color tempColor = ballImage.color;
        tempColor.a = value;
        ballImage.color = tempColor;
    }
}
