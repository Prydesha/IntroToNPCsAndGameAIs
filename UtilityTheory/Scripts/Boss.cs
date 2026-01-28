using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Inspector Vars
    [SerializeField] private List<Action> _actions = new List<Action>();
    [Tooltip("After the AI has switched between two distinct actions, " +
        "it must wait at least this amount of time before it can " +
        "attempt to switch to a different action")]
    [Min(0f)]
    [SerializeField] private float _minActionSwitchTime = 5f;
	[Header("External References")]
    [SerializeField] private Text _debugBehaviorText = null;
    [SerializeField] private bool _debugBehavior = true;
    
    // True Private Vars
    private Action _currentAction = null;
    private float _timeInCurrentAction = 0f;
    private Coroutine _utilityUpdater = null;
    private const float UtilityUpdateRate = 0.05f; // seconds

    public List<Action> Actions => _actions;

    /// <summary>
    /// Wrapper for the action currently being performed by this boss
    /// </summary>
    public Action CurrentAction
    {
        get => _currentAction;
        private set
        {
            // exit old action
            if (_currentAction)
            {
                _currentAction.OnExitAction(this);
            }
            // enter new action
            _currentAction = value;
            if (_currentAction)
            {
                _currentAction.OnEnterAction(this);
                SetDebugBehaviorText(_currentAction.name);
            }
            _timeInCurrentAction = 0f;
        }
    }

    /* METHODS */

    /// <summary>
    /// Checks to start a new behavior at a fixed rate
    /// </summary>
    /// <returns></returns>
    private IEnumerator UtilityUpdater()
    {
	    while (gameObject.activeSelf)
	    {
		    // determine if we should/can switch actions
		    if (_timeInCurrentAction >= _minActionSwitchTime && 
		        (!_currentAction || _timeInCurrentAction >= _currentAction.MinimumPerformTime))
		    {
                Action bestAction = null;
                float bestUtility = -1f;
                foreach (var action in _actions)
                {
                    var u = action.GetUtility(this);
                    if (u > bestUtility)
                    {
                        bestAction = action;
                        bestUtility = u;
                    }
                }
                if (bestAction)
                {
                    CurrentAction = bestAction; //< switch behaviors
                }
            }
		    yield return new WaitForSeconds(UtilityUpdateRate);
	    }
    }

    /// <summary>
    /// Setter for the text displayed above the boss
    /// </summary>
    /// <param name="txt"></param>
    public void SetDebugBehaviorText(string txt)
    {
	    if (_debugBehaviorText)
	    {
		    _debugBehaviorText.text = txt;
	    }
    }

    private void Awake()
    {
        if (_debugBehaviorText)
        {
	        _debugBehaviorText.gameObject.SetActive(_debugBehavior);
        }
    }

    private void Start()
    {
        _utilityUpdater = StartCoroutine(UtilityUpdater());
    }

    void Update()
    {
        if (_currentAction)
        {
            _timeInCurrentAction += Time.deltaTime;
            _currentAction.ActionUpdate(this);
        }
    }

    private void OnDestroy()
    {
        if (_utilityUpdater != null)
        {
            StopCoroutine(_utilityUpdater);
        }
    }

    private void OnEnable()
    {
        if (_utilityUpdater == null)
        {
            _utilityUpdater = StartCoroutine(UtilityUpdater());
        }
    }

    private void OnDisable()
    {
        if (_utilityUpdater != null)
        {
            StopCoroutine(_utilityUpdater);
        }
    }
}
