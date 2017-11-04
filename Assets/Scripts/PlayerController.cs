using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax; 
}


public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public float fireRate;
    private float fireRateDynamic;
    public float fireRateSwitchLimit;
    public float fireRateDecrement;

	public Boundary boundary;
	private Rigidbody playerRigidBody;
	public GameObject shot;
	public Transform shotSpawn;
    private int shotCount = 0;
	private float nextFire = 0.0f;

	void Start(){
		playerRigidBody = GetComponent<Rigidbody> ();
        fireRateDynamic = fireRate;
	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire)
        {
            EnageFire();
            GetComponent<AudioSource>().Play();
        }
    }

    private void EnageFire()
    {
        if (fireRateDynamic <= fireRateSwitchLimit )
        {
            shotCount++;
            fireRateDynamic = fireRate;
        }
        switch (shotCount)
        {
            case 0:
                Fire(0);
                break;
            case 1:
                Fire(-1);
                Fire(1);
                break;
            case 2:
                Fire(-0.25f);
                Fire(0.25f);
                Fire(0);
                break;
            default:
                Fire(-1);
                Fire(1);
                Fire(0);
                break;
        }
    }

    private void Fire(float displace)
    {
        nextFire = Time.time + fireRateDynamic;
        Vector3 dynamicShotSpawn = new Vector3(shotSpawn.position.x + displace, shotSpawn.position.y, shotSpawn.position.z);
        Instantiate(shot, dynamicShotSpawn, shotSpawn.rotation);
    }

    public void IncreaseFireRate()
    {
        if(shotCount < 3)
        {
            fireRateDynamic -= Mathf.Abs(fireRateDecrement);
        }else
        {
            GameObject gameControllerObj = GameObject.FindWithTag("GameController");
            if(gameControllerObj != null)
            {
                GameController gameController = gameControllerObj.GetComponent<GameController>();
                gameController.IncreaseDelayDecrement();
            }
        }
    }

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		playerRigidBody.velocity = movement * speed;

		playerRigidBody.position = new Vector3 
		(
				Mathf.Clamp(playerRigidBody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(playerRigidBody.position.z, boundary.zMin, boundary.zMax)
		);

		playerRigidBody.rotation = Quaternion.Euler (0.0f, 0.0f, playerRigidBody.velocity.x * -tilt);
	}
}
