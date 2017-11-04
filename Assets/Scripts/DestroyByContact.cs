using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;
    public int scoreValue;

    void Start()
    {
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
        if ((gameObject.CompareTag("BoltEnemy") && other.gameObject.CompareTag("Enemy"))
            || (gameObject.CompareTag("Enemy") && other.gameObject.CompareTag("BoltEnemy")))
            return;
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            if (gameController != null)
            {
                gameController.GameOver();
            }
        }
        if (gameController != null)
        {
            gameController.AddScore(scoreValue);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);

    }
}
