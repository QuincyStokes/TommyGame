using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScenario", menuName = "ScriptableObject/Scenario")]
public class Scenario : ScriptableObject
{
    //* ------------ References --------------- */
    [Header("Public References")]
    public GameObject levelPrefab;
    public Scenario option1Scenario;
    public Scenario option2Scenario;
    public GameObject speaker2Scene;

    //* ------------ Scenario-Specific Settings --------------- */
    [Tooltip("List of positions for the player to move to upon choosing an option.")]
    [Header("Decision Positions")]
    public List<Vector2> scenarioStartPositions;
    public List<Vector2> option1Positions;
    public List<Vector2> option2Positions;
    public string option1Text;
    public string option2Text;
    public List<string> scenarioDialogue;
    public List<string> option1Dialogue;
    public List<string> option2Dialogue;
    public float decisionTime;
    public bool isCutscene;
    public bool isBadEnding; //showing the continue screen
    public bool isGoodEnding; //fade to black 
    public bool hasSpeakerScene;

    //*-------------- Player ----------------- */
    public float playerMoveSpeed;
    public Vector2 playerStartPosition;
    public Vector2 playerScale = new Vector2(1, 1);


    //* -------------- Events ------------ */
    public event Action OnScenarioFinished;



    private void Start()
    {

    }

}
