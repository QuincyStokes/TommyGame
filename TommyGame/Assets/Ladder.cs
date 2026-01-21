using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y+2, player.position.z);
        }
    }
}
