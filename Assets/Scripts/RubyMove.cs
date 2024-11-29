using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    GameObject ruby;
    private float maxSpeed = 5;
    private float jumpPower = 5;
    private bool isjumping = false;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ruby = GetComponent<GameObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isRun", false);
        }
        else
        {
            anim.SetBool("isRun", true);
        }

        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isjumping == true)
            {
                return;
            }
            isjumping = true;
            rigid.AddForce(Vector2.up* jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
    }

    private void FixedUpdate()
    {
        // Ruby Move
        float h = Input.GetAxisRaw("Horizontal");

        // Debug.Log(h);
        if (isjumping == false)
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // Ruby Max Speed
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);

        }

        // Ground detect
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position,
                Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    Debug.Log(rayHit.collider.name);
                    anim.SetBool("isJump", false);
                    isjumping = false;
                }
            }
        }
    }
}
