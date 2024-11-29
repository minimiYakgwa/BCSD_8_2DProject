using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    private int nextMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Think();
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


    }

    void Think()
    {
        nextMove = Random.Range(-1, 1);
        Debug.Log(nextMove);
        Invoke("Think", 5);
    }
}
