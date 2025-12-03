using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingImage : MonoBehaviour
{
    public float scrollSpeed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
    }
}
