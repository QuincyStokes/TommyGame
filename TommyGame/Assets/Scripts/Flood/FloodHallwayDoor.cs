using UnityEngine;

public class FloodHallwayDoor : MonoBehaviour
{

    [SerializeField] private Animator animator;
    

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("Open", true);
        }
    }
}
