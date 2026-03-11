using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLadderScenario", menuName = "ScriptableObject/LadderScenario")]

public class LadderMinigameScenario : WinConditionalScenario
{
    private GameObject ladder;
    private Player player;
    
    public override void Initialize(GameObject scenarioObject)
    {
        base.Initialize(scenarioObject);
        ladder = scenarioObject.transform.Find("Ladder").gameObject;

        if(ladder != null)
        {
            if(ladder.TryGetComponent(out LadderTapController ltc))
            {
                ltc.OnTapsCompleted += HandleLadderTapsCompleted;
                //Do another one for when the time runs out
            }

        }
    }

    private void HandleLadderTapsCompleted()
    {
        InvokeScenarioCompleted();
    }



}
