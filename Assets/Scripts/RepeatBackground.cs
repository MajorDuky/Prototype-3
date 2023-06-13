using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 initialPosition;
    private float colliderLength;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        colliderLength = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= initialPosition.x - colliderLength)
        {
            transform.position = initialPosition;
        }
    }
}
