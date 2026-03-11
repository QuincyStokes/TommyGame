using UnityEngine;
using TMPro;
using System;

public class LadderTapController : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private TMP_Text progressCounter;

    [SerializeField] private int stepAmount;
    private float progress;
    private Vector3 startPos;
    private Quaternion startRot; 
    private int targetTapAmount;
    private int taps = 0;

    public event Action OnTapsCompleted;
    public event Action OnTapsFailed;
    

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        targetTapAmount = 100 / stepAmount;
        progressCounter.text = $"{taps} / {targetTapAmount}";
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && taps < targetTapAmount)
        {
            OnTap();
        }
    }

    public void OnTap()
    {
        progress += stepAmount;
        float t = progress / 100;
        transform.SetPositionAndRotation(Vector3.Lerp(startPos, targetPosition, t), Quaternion.Lerp(startRot, targetRotation, t));
        UpdateTapCounter();
        CheckTapCompletion();
    }

    private void UpdateTapCounter()
    {
        taps++;
        progressCounter.text = $"{taps} / {targetTapAmount}";
    }

    private void CheckTapCompletion()
    {
        if(taps == targetTapAmount)
        {
            //do logic for the completion
            progressCounter.color = Color.green;
            Debug.Log("Taps Completed!");
            OnTapsCompleted?.Invoke();
        }
    }
}
