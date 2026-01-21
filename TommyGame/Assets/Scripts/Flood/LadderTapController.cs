using UnityEngine;

public class LadderTapController : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Quaternion targetRotation;

    [SerializeField] private float stepAmount;
    private float progress;
    private Vector3 startPos;
    private Quaternion startRot;

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            OnTap();
        }
    }

    public void OnTap()
    {
        progress += stepAmount;
        float t = progress / 100;
        transform.SetPositionAndRotation(Vector3.Lerp(startPos, targetPosition, t), Quaternion.Lerp(startRot, targetRotation, t));
    }
}
