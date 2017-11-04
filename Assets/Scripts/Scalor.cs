using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scalor : MonoBehaviour
{
    public float scaleSpeed;
    // Update is called once per frame
    void Update()
    {
        float lScale = (Mathf.Sin(5 * Time.time) * scaleSpeed) + scaleSpeed + 1;
        transform.localScale = new Vector3(lScale, lScale, lScale);
    }
}
