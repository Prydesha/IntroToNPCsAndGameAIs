using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public abstract class Action : ScriptableObject
{
    [Range(0f, 0.999f)] 
    protected float _minimumUtility = 0f;
    [Range(0.001f, 1f)] 
    protected float _maximumUtility = 1f;

    [Tooltip("The elements which affect our behavior")]
	[SerializeField] 
    private List<Factor> _factors = null;

    [Tooltip("As long as the utility of this attack is non-zero, this " +
        "amount can be randomly added to its calculated utility")] 
    [Range(0f, 1f)]
    [SerializeField]
    private float _randomUtilBonusMax = 0.01f;

	[Tooltip("After a boss decides to perform this behavior, it must continue " +
        "to do so for this amount of time before considering any other actions")]
    [Min(0f)]
    [SerializeField]
    private float _minimumPerformTime = 0.5f;
    
    // for debugging
    [HideInInspector] 
    public float lastCalculatedUtility = 0f;

    public List<Factor> Factors => _factors;
    public float MinimumPerformTime => _minimumPerformTime;

    public float GetUtility(Boss boss)
    {
        lastCalculatedUtility = 0f; //< for debugging
        if (_factors == null 
            || _factors.Count < 1)
        {
            return 0f;
        }
        float numerator = 0f;
        float denominator = 0f;
        foreach (var factor in _factors)
        {
            numerator += factor.GetWeightedValue(boss, this);
            denominator += factor.Weight;
        }
        if (denominator == 0) 
        { 
            return 0f; 
        }
        var u = numerator / denominator;
        if (u > 0)
        {
            u += Random.Range(0f, _randomUtilBonusMax);
        }
        u = Mathf.Clamp(u, _minimumUtility, _maximumUtility);
        lastCalculatedUtility = u; //< for debugging
        return u;
    }

    public virtual void OnEnterAction(Boss boss) { }
    public virtual void ActionUpdate(Boss boss) { }
    public virtual void OnExitAction(Boss boss) { }
}