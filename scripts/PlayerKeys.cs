using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    [SerializeField] AudioClip doorOpenSFX;
    [SerializeField] AudioClip doorCloseSFX;
    [SerializeField] AudioClip keyCollectSFX;
    float SFXvolume;
    List<Key.KeyType> keys;
    KeyDoor keyDoor;
    bool isNearDoor;
    bool haveKeyforDoor;
    bool nearbyDoorNeedKey;

    void Awake()
    {
        keys = new List<Key.KeyType>();
    }

    private void Start()
    {
        SFXvolume = PlayerPrefsController.GetGameVolume();
    }

    private void Update()
    {
        if(isNearDoor)
        {
            if(haveKeyforDoor || !nearbyDoorNeedKey)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (!keyDoor.DoorOpen)
                    {
                        keyDoor.OpenDoor();
                        AudioSource.PlayClipAtPoint(doorOpenSFX, Camera.main.transform.position, SFXvolume);
                    }
                    else
                    {
                        keyDoor.CloseDoor();
                        AudioSource.PlayClipAtPoint(doorCloseSFX, Camera.main.transform.position, SFXvolume);
                    }
                    
                }
            }
        }
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keys.Remove(keyType);
    }

    public void AddKey(Key.KeyType keyType)
    {
        keys.Add(keyType);
    }

    public bool HaveKey(Key.KeyType keyType)
    {
        return keys.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Key key = other.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(keyCollectSFX, Camera.main.transform.position, SFXvolume);
            Debug.Log("Added Key");
        }

        keyDoor = other.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            isNearDoor = true;
            nearbyDoorNeedKey = keyDoor.NeedKey();
            if(nearbyDoorNeedKey)
            {
                if (HaveKey(keyDoor.GetKeyType()))
                {
                    haveKeyforDoor = true;
                }
                else
                {
                    haveKeyforDoor = false;
                }
            }
        }
        else
        {
            isNearDoor = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isNearDoor = false;
        haveKeyforDoor = false;
        keyDoor = null;
    }
}