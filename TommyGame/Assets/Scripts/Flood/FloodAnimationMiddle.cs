using UnityEngine;

public class FloodAnimationMiddle : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if(player != null)
        {
            Debug.Log("Player found, listening to die!");
            //ScenarioManager.Instance.OnOption1MovementEnded += player.Die;
        }
        else
        {
            Debug.Log("Player not found.");    
        }
    }

    private void OnDestroy()
    {
        if(player != null)
        {
            ScenarioManager.Instance.OnOption2MovementEnded -= player.Die;
        }
    }
}
