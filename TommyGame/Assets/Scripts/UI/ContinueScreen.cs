using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScreen : MonoBehaviour
{

    public TMP_Text timeRemaining;
    public void LeaveGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnEnable()
    {
        StartCoroutine(DoContinueTimer(20));
    }

    private IEnumerator DoContinueTimer(float time)
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
        LeaveGame();

    }
}
