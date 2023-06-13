using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : MonoBehaviour
{
    public float speed;
    private PlayerController playerController;
    private float startGameTimer;
    private bool startAnimationStatus;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        startGameTimer = 1;
        startAnimationStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGameTimer > 0 && !startAnimationStatus && gameObject.CompareTag("Background"))
        {
            startGameTimer -= Time.deltaTime;
        }
        else if (startGameTimer <= 0 && !startAnimationStatus && gameObject.CompareTag("Background"))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
            startAnimationStatus = true;
        }
        else
        {
            if (!playerController.gameOver)
            {
                transform.Translate(speed * Time.deltaTime * Vector3.left);
            };
        }
    }
}
