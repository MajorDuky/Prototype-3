using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private float timer;
    private Vector3 spawnPosition;
    private PlayerController playerController;
    private float startGameTimer;
    private bool startAnimationStatus;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(30, 0, 0);
        timer = 2;
        startGameTimer = 1;
        startAnimationStatus = false;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startGameTimer > 0 && !startAnimationStatus)
        {
            startGameTimer -= Time.deltaTime;
        }
        else if (startGameTimer <= 0 && !startAnimationStatus)
        {
            Invoke(nameof(SpawnObstacle), timer);
            startAnimationStatus = true;
        }
        else
        {
            return;
        }
    }

    private void SpawnObstacle()
    {
        if (!playerController.gameOver)
        {
            timer = Random.Range(1.5f, 3f);
            int randomIndex = Random.Range(0, propPrefabs.Length);

            GameObject clonedProp = Instantiate(propPrefabs[randomIndex], spawnPosition, propPrefabs[randomIndex].transform.rotation);
            Invoke(nameof(SpawnObstacle), timer);
        }

    }
}
