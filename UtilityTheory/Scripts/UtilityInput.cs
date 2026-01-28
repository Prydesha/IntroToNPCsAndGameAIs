using UnityEngine;
/** 
 * Represents a game element which affects the utility 
 * of an action or choice made by our boss AI
 */
[System.Serializable]
public abstract class UtilityInput : ScriptableObject
{
    // Override to provide a 0-1 value for this factor's utility function
    public abstract float GetInput(Boss boss);
}

