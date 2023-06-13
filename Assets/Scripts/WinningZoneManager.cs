using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningZoneManager : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.IncreaseScore();
        }
        
    }
}
