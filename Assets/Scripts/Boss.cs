using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    GameObject walls;

    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider;
    Rigidbody2D rigid;

    public int currentHP = 10;

    private void Awake()
    {
        walls.SetActive(true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //AttackToPlayer();
        //AttackLaser();
    }

    private IEnumerator AttackLaser()
    {

        if (!isPossibleToAttackTarget())
        {
            DisableLaser();
        }
        EnableLaser();

        AttackToPlayer();

        yield return null;
    }

    private bool isPossibleToAttackTarget()
    {
        float distanceX = (float)Mathf.Pow((player.transform.position.x - this.transform.position.x), 2);
        float distanceY = (float)Mathf.Pow((player.transform.position.y - this.transform.position.y), 2);
        float distance = (float)Mathf.Sqrt((distanceX + distanceY));
        Debug.Log(distance);
        if (distance <= 20)
            return true;
        else return false;
    }

    private void AttackToPlayer()
    {
        while (true)
        {   
            lineRenderer.SetPosition(0, this.transform.position);
        
            lineRenderer.SetPosition(1, player.transform.position);

        }
        
    }

    private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
    }

    private void DisableLaser()
    {
        lineRenderer.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player" && collision.transform.position.y > this.transform.position.y + 1)
            || collision.gameObject.tag == "Bullet")
            Ondamaged();
    }

    private void Ondamaged()
    {
        if (GameManager.Instance.UpdateBossHealth())
            EnemyDead();
        spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        this.gameObject.layer = 8;
        Invoke("Recovery", 0.5f);
            
    }

    private void Recovery()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        this.gameObject.layer = 9;
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
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        GameManager.Instance.playTime -= 1f;
        Invoke("DeActivate", 5);
        walls.SetActive(false);
    }
}
