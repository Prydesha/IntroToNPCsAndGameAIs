using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI Inputs/DistanceFromPlayer", 
    fileName = "Input_DistanceFromPlayer.asset")]
public class InputDistanceFromPlayer : UtilityInput
{
    public override float GetInput(Boss boss)
    {
        return (boss.transform.position - GetPlayerPosition()).magnitude / 50f;
    }

    private Vector3 GetPlayerPosition()
    {
        return Vector3.zero;
    }
}
