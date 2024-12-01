using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Mono.Cecil.Cil;
using UnityEngine;

public class RubyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    GameObject ruby;
    private float maxSpeed = 5;
    private float jumpPower = 15;
    private bool isjumping = false;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private bool isDamage = false;
    private int jumpCount = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ruby = GetComponent<GameObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetButtonUp("Horizontal") && isDamage == false)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (rigid.velocity.normalized.x == 0 )
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
            if (jumpCount >= 1)
                isjumping = true;
            jumpCount++;
            rigid.AddForce(Vector2.up* jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
    }

    private void FixedUpdate()
    {   
        float h = Input.GetAxisRaw("Horizontal");

        if (isDamage == false){
            // Ruby Move
            if (isjumping == false)
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            else
                rigid.AddForce(Vector2.right * h * 0.5f, ForceMode2D.Impulse);
            // Ruby Max Speed
            if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
                {
                    rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);
                }
        }
        

        // Ground detect
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            Vector2 rightVec = new Vector2(rigid.position.x, rigid.position.y + 1);
            Debug.DrawRay(rightVec, h == 1 ? Vector3.right : Vector3.left, new Color(0, 1, 1));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position,
                Vector3.down, 1, LayerMask.GetMask("Platform"));
            RaycastHit2D rayHit2 = Physics2D.Raycast(rightVec, h == 1 ? Vector3.right : Vector3.left,
                1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    //Debug.Log(rayHit.collider.name);
                    anim.SetBool("isJump", false);
                    isDamage = false;
                    isjumping = false;
                    jumpCount = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("플레이어가 맞았음.");
            OnDamaged(collision.transform.position);
        }
    }

    private void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Knock-Back direction 
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

        //피격 애니메이션 실행
        anim.SetTrigger("doDamaged"); 
        isDamage = true;
        // 무적시간 설정
        Invoke("OffDamaged", 3);
    }

    private void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
