using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offset;

    private void Start()
    {
        myMaterial = GetComponent<MeshRenderer>().material;
        offset = new Vector2(scrollSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
