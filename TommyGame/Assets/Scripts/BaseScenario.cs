using UnityEngine;

public class BaseScenario : MonoBehaviour
{
    [HideInInspector] public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    public virtual void Start()
    {

    }

    
}
