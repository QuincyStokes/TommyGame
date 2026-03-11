using UnityEngine;

public class FloodHallwayBrain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        GameObject flood = GameObject.Find("FloodWater");
        if(flood != null)
            flood.SetActive(false);
    }
}
