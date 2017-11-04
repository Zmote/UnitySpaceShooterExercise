using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusContact : MonoBehaviour
{
    public GameObject explosion;
    private GameController gameController;
    private PlayerController player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Couldn't load 'Player' object");
        }
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            if (player != null)
            {
                player.IncreaseFireRate();
                gameController.AddScore(5);
            }
        }
    }
}
