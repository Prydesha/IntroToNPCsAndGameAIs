using UnityEngine;
[System.Serializable]
public class Factor
{
    [Min(0f)] 
    public float Weight = 1f;
    [Tooltip("Equation used to determine the utility of this factor. " +
        "Keep all values within 0 and 1")]
    public AnimationCurve UtilityFunction = AnimationCurve.Linear(0, 0, 1, 1);
    public UtilityInput Input;

    public float GetWeightedValue(Boss boss, Action action)
    {
        if (Input == null)
        {
            Debug.LogError($"Detected null input for a factor on [{action.name}]");
            return 0f;
        }
        float clampedValue = Mathf.Clamp01(Input.GetInput(boss));
        float equationValue = Mathf.Clamp01(UtilityFunction.Evaluate(clampedValue));
        return equationValue * Weight;
    }
}