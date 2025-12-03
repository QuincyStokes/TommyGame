using UnityEngine;

public class FloodWaterClearer : MonoBehaviour
{
    private GameObject floodWater;
    private void Awake()
    {
        floodWater = GameObject.Find("FloodWater");
        if(floodWater != null)
        {
            floodWater.SetActive(false);
        }
    }
}
