using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
  

    private float maxShotDelay = 0.15f;
    private float curShotDelay = 5;
    private float maxSpeed = 5;
    private float jumpPower = 15;
    private int jumpCount = 0;
    private bool isjumping = false;
    private bool isDamage = false;
    private bool isFallDown = false;
    

    [SerializeField]
    private GameObject bulletOb;
    [SerializeField]
    private PlayerSceneManager playerSceneManager;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveAndAnimation();
        Fire();
        Reload();

        IsFallDown();
    }

    private void FixedUpdate()
    {
        PhysicsMove();

        GroundDetect();
    }

    private void GroundDetect()
    {
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            Vector2 downVec2 = new Vector2(rigid.position.x + 0.5f, rigid.position.y);
            Debug.DrawRay(downVec2, Vector3.down, new Color(0, 1, 0));

            Vector2 downVec3 = new Vector2(rigid.position.x - 0.5f, rigid.position.y);
            Debug.DrawRay(downVec3, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position,
                Vector3.down, 1, LayerMask.GetMask("Platform"));

            RaycastHit2D rayHit2 = Physics2D.Raycast(downVec2,
                Vector3.down, 0.1f, LayerMask.GetMask("Platform"));

            RaycastHit2D rayHit3 = Physics2D.Raycast(downVec3,
                Vector3.down, 0.1f, LayerMask.GetMask("Platform"));

            /*
             * Vector2 rightVec = new Vector2(rigid.position.x, rigid.position.y + 1);
               Debug.DrawRay(rightVec, h == 1 ? Vector3.right : Vector3.left, new Color(0, 1, 1));

             * RaycastHit2D rayHit2 = Physics2D.Raycast(rightVec, h == 1 ? Vector3.right : Vector3.left,
                1, LayerMask.GetMask("Platform"));*/


            if (rayHit.collider != null || rayHit2.collider != null || rayHit3.collider != null)
            {
                if (rayHit.distance < 0.5f || rayHit2.distance < 0.5f || rayHit3.distance < 0.5f)
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
    private void PhysicsMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (isDamage == false)
        {
            if (isjumping == false)
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            else
                rigid.AddForce(Vector2.right * h * 0.5f, ForceMode2D.Impulse);

            if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);
        }
    }

    private void MoveAndAnimation()
    {
        if (Input.GetButtonUp("Horizontal") && isDamage == false)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("isRun", false);
        else
            anim.SetBool("isRun", true);

        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        if (Input.GetButtonDown("Jump"))
        {
            if (isjumping == true)
                return;

            if (jumpCount >= 1)
                isjumping = true;

            jumpCount++;

            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            anim.SetBool("isJump", true);
        }
    }

    private void Fire()
    {
        if (curShotDelay < maxShotDelay || !Input.GetButtonDown("Fire")) 
            return;
        
        anim.SetTrigger("doAttack");
        Vector2 vec = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        GameObject bullet = Instantiate(bulletOb, vec, this.transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce((spriteRenderer.flipX == true ? Vector2.right : Vector2.left) * 10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime * 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (this.transform.position.y > collision.transform.position.y + 1)
                OnAttackHead();
            else
                OnDamaged(collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cliff")
            isFallDown = true;
    }

    private void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*10, ForceMode2D.Impulse);

        anim.SetTrigger("doDamaged"); 
        isDamage = true;

        GameManager.Instance.UpdatePlayerHealth();

        Invoke("OffDamaged", 3);
    }

    private void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void OnAttackHead()
    {
        jumpCount -= 1;
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    private void IsFallDown()
    {
        if (!isFallDown) 
            return;
        Debug.Log("³¶¶°·¯Áö·Î ¶³¾îÁü;;");
        GameManager.Instance.UpdatePlayerHealth(true);
        //transform.Rotate(new Vector2(0, 100 * Time.deltaTime));
    }
}
