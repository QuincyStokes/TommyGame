using UnityEngine;

[CreateAssetMenu(fileName = "NewHelicopterScenario", menuName = "ScriptableObject/HelicopterScenario")]

public class HelicopterMinigameScenario : WinConditionalScenario
{
    private GameObject helicopter;
    private Player player;
    
    public override void Initialize(GameObject scenarioObject)
    {
        base.Initialize(scenarioObject);
        helicopter = GameObject.Find("FloodRooftop(Clone)");

        if(helicopter != null)
        {
            if(helicopter.TryGetComponent(out HelicopterTapController htc))
            {
                htc.OnTapsCompleted += HandleHelicopterTapsCompleted;
            }
            else
            {
                Debug.Log("Found helicopter but no HelicopterTapController object.");
            }

        }
        else
        {
            Debug.Log("Helicopter was null.");
        }

    }

    private void HandleHelicopterTapsCompleted()
    {
        InvokeScenarioCompleted();
    }
}
