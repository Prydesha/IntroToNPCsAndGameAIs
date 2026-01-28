using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI Actions/SwingSword", 
    fileName = "SwingSwordAction.asset")]
public class ActionSwingSword : Action
{
    public override void OnEnterAction(Boss boss)
    {
        base.OnEnterAction(boss);
        Debug.LogWarning("I'm swinging my sword!");
    }

    public override void OnExitAction(Boss boss)
    {
        base.OnExitAction(boss);
        Debug.Log("My sword is swung.");
    }
}
