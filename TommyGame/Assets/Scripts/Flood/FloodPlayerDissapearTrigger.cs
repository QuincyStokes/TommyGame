using UnityEngine;

public class FloodPlayerDissapearTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
