using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    private int nextMove;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        Think();
    }

    private void Update()
    {
        if (nextMove != 0)
        {
            anim.SetBool("isRunning", true);
            if (nextMove == 1)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }

        else
        {
            anim.SetBool("isRunning", false);
        }

        
    }

    private void FixedUpdate()
    {
        // Move 
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform check (Down Platform)
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y );
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 1));
        
        //Platform check (Horizontal Platform)
        Vector2 rightVec = new Vector2(rigid.position.x, rigid.position.y + 1);
        Debug.DrawRay(rightVec, nextMove == 1 ? Vector3.right : Vector3.left, new Color(0, 1, 1));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        RaycastHit2D rayHit2 = Physics2D.Raycast(rightVec, nextMove == 1 ? Vector3.right : Vector3.left, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null || rayHit2.collider != null)
        {
            turn();
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void turn()
    {
        nextMove *= -1;
        CancelInvoke();
        Invoke("Think", 5);
    }
    void DeActivate()
    {
        gameObject.SetActive(false);
    }

    void EnemyDead()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        spriteRenderer.flipY = true;
        collider.enabled = false;
        rigid.AddForce(Vector2.up*5, ForceMode2D.Impulse);
        Invoke("DeActivate", 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Àû ÇÇ°Ý!!");
            EnemyDead();

        }
    }


}
