using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Animator _anim;

    public float moveSpeed;

    public void Initialize(Vector3 startPos, Vector3 startScale)
    {
        gameObject.SetActive(true);
        transform.position = startPos;
        transform.localScale = startScale;
    }
    public IEnumerator MoveTo(Vector3 position)
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
        
        while (Vector2.Distance(transform.position, position) > .2f)
        {
            print($"Distance to position:  {Vector2.Distance(transform.position, position)}");
            transform.Translate(moveSpeed * Time.deltaTime * dir.normalized);
            yield return null;
        }
        _anim.SetTrigger("Idle");
    }

}
