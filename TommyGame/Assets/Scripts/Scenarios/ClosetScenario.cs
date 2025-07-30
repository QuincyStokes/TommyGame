using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ClosetScenario : BaseScenario
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public SpriteRenderer door;
    public SpriteRenderer openedDoor;
    public Sprite doorOpen;

    public override void Start()
    {
        base.Start();
        StartCoroutine(DoorOpen());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator DoorOpen()
    {
        //player runs
        yield return new WaitForSeconds(2.5f);

        //after 2.5 seconds, simulate opening the door
        door.gameObject.SetActive(false);
        openedDoor.gameObject.SetActive(true);

        //player reachces the door and disappears
        yield return new WaitForSeconds(1f);
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(.3f);

        //door closes again.
        door.gameObject.SetActive(true);
        openedDoor.gameObject.SetActive(false);
    }
}
