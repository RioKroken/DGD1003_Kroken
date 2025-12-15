using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;

    private Collider2D doorCollider;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        CloseDoor(); // Oyun baþlarken kapalý
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;

        if (doorCollider != null)
            doorCollider.enabled = true;
    }

    public void CloseDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = closedDoorSprite;

        if (doorCollider != null)
            doorCollider.enabled = false;
    }
}
