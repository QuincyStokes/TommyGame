using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    //* -------------------- UI References ----------------------- */
    [Header("References")]
    public Button button1;
    public Button button2;
    public TMP_Text button1Text;
    public TMP_Text button2Text;
    public TMP_Text dialogueText;
    public GameObject dialogueParent;
    public TMP_Text timeRemaining;
    public GameObject continueScreen;
    public Image blackScreen;

    //* ------------------- Scenarios ------------------------- */
    [Header("Scenarios")]
    public List<Scenario> Scenarios;
    public Scenario currentScenario;
    //* ---------------- Settings --------------- */
    public float textSpeed;
    public float fadeTime;

    //* -------------- Player ---------- */
    public Player player;


    //* -------------- Internal ---------------- */
    private Coroutine timerCoroutine;
    private GameObject currentScenarioObject;
    private void Start()
    {
        LoadScenario(Scenarios[0]);
        button1.onClick.AddListener(HandleButton1Clicked);
        button2.onClick.AddListener(HandleButton2Clicked);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }


    /// <summary>
    /// Loads a given scenario
    /// Called from Scenarios themselves when a decision is made (button pressed)
    /// </summary>
    /// <param name="scenario">BaseScenario to load</param>
    private void LoadScenario(Scenario scenario)
    {
        currentScenario = scenario;
        HideButtons();
        timeRemaining.gameObject.SetActive(false);
        StartCoroutine(RunScenario(scenario));
    }

    private IEnumerator RunScenario(Scenario scenario)
    {
        
        button1Text.text = scenario.option1Text;
        button2Text.text = scenario.option2Text;
        player.Initialize(currentScenario.playerStartPosition, currentScenario.playerScale);
        currentScenarioObject = Instantiate(scenario.levelPrefab);
        yield return DoMovement(scenario.scenarioStartPositions);
        yield return StartCoroutine(DoDialogue(scenario.scenarioDialogue));
    }

    private void ShowButtons()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

    }

    private void HideButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }
    private void HandleButton1Clicked()
    {
        StartCoroutine(Button1Clicked());
    }
    private void HandleButton2Clicked()
    {
        StartCoroutine(Button2Clicked());
    }
    private IEnumerator Button1Clicked()
    {
        StopCoroutine(timerCoroutine);
        HideButtons();
        yield return StartCoroutine(DoDecisionDialogue(currentScenario.option1Dialogue));
        yield return StartCoroutine(DoMovement(currentScenario.option1Positions));
        yield return StartCoroutine(NextScenario(currentScenario.option1Scenario));
    }
    private IEnumerator Button2Clicked()
    {
        StopCoroutine(timerCoroutine);
        HideButtons();
        yield return StartCoroutine(DoDecisionDialogue(currentScenario.option2Dialogue));
        yield return StartCoroutine(DoMovement(currentScenario.option2Positions));
        yield return StartCoroutine(NextScenario(currentScenario.option2Scenario));
    }

    private IEnumerator PlayCutscene()
    {
        yield return StartCoroutine(DoMovement(currentScenario.option1Positions));
        yield return StartCoroutine(NextScenario(currentScenario.option1Scenario));
    }


    private IEnumerator DoMovement(List<Vector2> positions)
    {
        foreach (Vector2 pos in positions)
        {
            yield return StartCoroutine(player.MoveTo(pos));
        }

        //wait a second, and then move to next scenario
        yield return new WaitForSeconds(1f);

    }

    private IEnumerator DoDialogue(List<string> dialogue)
    {
        print("Doing Dialogue");
        dialogueText.text = "";
        dialogueParent.SetActive(true);
        
        foreach (string str in dialogue)
        {
            foreach (char c in str)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            yield return new WaitForSeconds(1.5f);
            
            dialogueText.text = "";
        }
        dialogueParent.SetActive(false);
        if (!currentScenario.isCutscene && !currentScenario.isGoodEnding && !currentScenario.isBadEnding)
        {
            ShowButtons();
            timerCoroutine = StartCoroutine(DoScenarioTimer(currentScenario.decisionTime));
        }

        if (currentScenario.isCutscene)
        {
            StartCoroutine(PlayCutscene());
        }

        if (currentScenario.isBadEnding)
        {
            continueScreen.SetActive(true);
        }

        if (currentScenario.isGoodEnding)
        {
            StartCoroutine(FadeToBlack());
        }


    }

    private IEnumerator DoDecisionDialogue(List<string> dialogue)
    {
        dialogueText.text = "";
        dialogueParent.SetActive(true);

        int speakerCounter = 1;
        GameObject speaker2Scene = null;
        if (currentScenario.hasSpeakerScene)
        {
            speaker2Scene = Instantiate(currentScenario.speaker2Scene);
            speaker2Scene.SetActive(false);
        }

        foreach (string str in dialogue)
        {
            if (currentScenario.hasSpeakerScene)
            {
                //Swap between speaker 1 and 2
                if (speakerCounter % 2 == 0)
                {
                    speaker2Scene.SetActive(true);
                }
                else
                {
                    speaker2Scene.SetActive(false);
                }
            }
            foreach (char c in str)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            speakerCounter++;
            yield return new WaitForSeconds(1.5f);
            dialogueText.text = "";
        }
        dialogueParent.SetActive(false);
        
        //Destroy speaker2scene
        Destroy(speaker2Scene);
    }

    private IEnumerator DoScenarioTimer(float time)
    {
        timeRemaining.gameObject.SetActive(true);
        float elapsed = 0f;
        timeRemaining.text = time.ToString("N2");
        while (elapsed <= time)
        {
            elapsed += Time.deltaTime;
            timeRemaining.text = (time - elapsed).ToString("N2");
            yield return null;
        }

        //if we get here, that means the timer ended without them choosing an option. Game Over

        GameOver();
    }

    private IEnumerator FadeToBlack()
    {
        float elapsed = 0f;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeTime;

            blackScreen.color = new Color(0, 0, 0, t);
            yield return null;
        }
        CleanupCurrentScenario();
        SceneManager.LoadScene("MainMenu");
    }

    private void GameOver()
    {
        CleanupCurrentScenario();
        HideButtons();
        continueScreen.SetActive(true);
    }

    private void CleanupCurrentScenario()
    {
        Destroy(currentScenarioObject);
    }

    private IEnumerator NextScenario(Scenario scenario)
    {
        CleanupCurrentScenario();
        yield return null;
        if(scenario != null)
            LoadScenario(scenario);
    }
}
