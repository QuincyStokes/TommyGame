using UnityEngine;

public class WalletDecisionAddon : MonoBehaviour
{
    [SerializeField] private Collider2D tripTrigger;

    private void Start()
    {
        ScenarioManager.Instance.OnOption1Pressed += HandleOption1Pressed;
    }

    private void HandleOption1Pressed()
    {
        Debug.Log("WalletDecisionAddon hears option1Pressed");
        tripTrigger.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("WalletDecsionAddion hears collision");
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Die();            
        }
    }
}
