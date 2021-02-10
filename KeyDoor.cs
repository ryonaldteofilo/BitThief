using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] Key.KeyType keyType;
    [SerializeField] Collider2D doorCollider;
    Animator doorAnimator;
    //SpriteRenderer leftDoorSprite;
    //SpriteRenderer rightDoorSprite;

    [SerializeField] bool needKey;
    public bool DoorOpen;

    private void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        DoorOpen = false;
    }

    public bool NeedKey()
    {
        return needKey;
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        DoorOpen = true;
        doorCollider.enabled = false;
        doorAnimator.SetBool("DoorOpen", DoorOpen);
    }

    public void CloseDoor()
    {
        DoorOpen = false;
        doorCollider.enabled = true;
        doorAnimator.SetBool("DoorOpen", DoorOpen);
    }
}
