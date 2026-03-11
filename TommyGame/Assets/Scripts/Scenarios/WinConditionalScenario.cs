using System;
using UnityEngine;

public class WinConditionalScenario : Scenario
{
    public event Action<WinConditionalScenario> OnScenarioCompleted;
    public event Action<WinConditionalScenario> OnScenarioLost;
    protected GameObject scenarioObject;

    public virtual void Initialize(GameObject scenarioObject)
    {
        this.scenarioObject = scenarioObject;
    }

    protected void InvokeScenarioCompleted()
    {
        OnScenarioCompleted?.Invoke(this);
    }
    
}

