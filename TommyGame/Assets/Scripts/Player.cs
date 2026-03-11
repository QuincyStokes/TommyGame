using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Animator _anim;

    public float moveSpeed;

    private float startingMoveSpeed;
    private bool isDead;

    public void Initialize(Vector3 startPos, Vector3 startScale)
    {
        gameObject.SetActive(true);
        transform.position = startPos;
        transform.localScale = startScale;
        startingMoveSpeed = moveSpeed;
    }

    public IEnumerator MoveTo(Vector3 position, float newMoveSpeed=0f)
    {
        print($"Player running to {position} ");
        _anim.SetTrigger("Run");
        Vector3 dir = position - transform.position;
        if (dir.x < 0)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }
        if(newMoveSpeed != 0)
        {
            moveSpeed = newMoveSpeed;
        }
        else
        {
            moveSpeed = startingMoveSpeed;
        }
        while (Vector2.Distance(transform.position, position) > .2f)
        {
            transform.Translate(moveSpeed * Time.deltaTime * dir.normalized);
            yield return null;
        }
        if(!isDead)
        {
            _anim.SetTrigger("Idle");
            
        }
    }

    public IEnumerator ClimbTo(Vector3 position, float newMoveSpeed=0f)
    {
        print($"Player running to {position} ");
        _anim.SetTrigger("Climb");
        Vector3 dir = position - transform.position;
        if (dir.x < 0)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }
        if(newMoveSpeed != 0)
        {
            moveSpeed = newMoveSpeed;
        }
        else
        {
            moveSpeed = startingMoveSpeed;
        }
        while (Vector2.Distance(transform.position, position) > .2f)
        {
            transform.Translate(moveSpeed * Time.deltaTime * dir.normalized);
            yield return null;
        }
        if(!isDead)
        {
            _anim.SetTrigger("Idle");
            
        }
    }

    public void Die()
    {
        isDead = true;
        StopAllCoroutines();
        _anim.ResetTrigger("Idle");
        _anim.ResetTrigger("Run");
        Debug.Log("Player Dying!");
        _anim.SetTrigger("Die");
    }

}
