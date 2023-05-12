using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WalkingMonster : Monster
{
    [SerializeField] private float speed = 3.5f;
    private int lives = 2;
    private Vector3 dir;
    private SpriteRenderer sprite;

    private void Start()
    {
        dir = transform.right;
        
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + 0.7f * dir.x * transform.right, 0.1f);

        if (colliders.Length > 0) 
        {
            dir *= -1f;
            sprite.flipX = dir.x > 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * 0.001f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives -= 1;
            Debug.Log("У врага" + lives);
        }
        if (lives < 1) Die();
    }


}
