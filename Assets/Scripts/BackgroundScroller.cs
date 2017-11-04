using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSize;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y,
        Mathf.Repeat(transform.position.z + (Time.deltaTime * scrollSpeed), tileSize));
    }
}
