using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
