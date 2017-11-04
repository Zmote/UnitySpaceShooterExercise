using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Vector2 shotDelayInSeconds;
    public float initialDelayInSeconds;

    public GameObject shot;
    public Transform shotSpawn;


    void Start()
    {
        GameObject gameControllerObj = GameObject.FindWithTag("GameController");
        if (gameControllerObj != null)
        {
            GameController gameController = gameControllerObj.GetComponent<GameController>();
            shotDelayInSeconds = new Vector2(shotDelayInSeconds.x, Mathf.Max(shotDelayInSeconds.x, shotDelayInSeconds.y - gameController.enemyShotDelayDecrement));
        }
        else
        {
            Debug.Log("Could not find 'GameController' object");
        }
        StartCoroutine(FireWeapon());
    }

    IEnumerator FireWeapon()
    {
        yield return new WaitForSeconds(initialDelayInSeconds);
        while (true)
        {
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.rotation);
            yield return new WaitForSeconds(Random.Range(shotDelayInSeconds.x, shotDelayInSeconds.y));
        }
    }

}
