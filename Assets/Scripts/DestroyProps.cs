using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProps : MonoBehaviour
{
    private float leftBound;
    // Start is called before the first frame update
    void Start()
    {
        leftBound = -10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= leftBound)
        {
            Destroy(gameObject);
        }
    }
}
