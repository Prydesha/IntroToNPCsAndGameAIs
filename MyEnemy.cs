using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Start,
        Idle,
        AttackingPlayer,
        Taunting,
        Perishing
    }

    private EnemyState _myState = EnemyState.Start;

    private void SetState(EnemyState newState)
    {
        // perform behavior associated with
        // LEAVING the previous state
        switch (_myState)
        {
            case EnemyState.AttackingPlayer:
                StopAttacking();
                break;
        }
        _myState = newState;
        // perform behavior associated with
        // ENTERING the new state
        switch (_myState)
        {
            case EnemyState.Perishing:
                PlayDeathAnimation();
                break;
        }
    }

    private void Start()
    {
        SetState(EnemyState.Idle);
    }

    private void Update()
    {
        // behavior to perform while in the active state
        switch (_myState)
        {
            case EnemyState.Idle:
                WalkAround();
                break;
        }
    }

    private void WalkAround()
    {

    }

    private void StopAttacking()
    {

    }

    private void PlayDeathAnimation()
    {

    }
}
