using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Update();
    public void Exit();
}

public class Idle : IState
{
    Animator anim;
    
    public void Enter()
    {

    }
    public void Update()
    {

    }
    public void Exit()
    {

    }
}