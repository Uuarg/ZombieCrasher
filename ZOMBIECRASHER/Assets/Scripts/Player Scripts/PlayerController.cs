using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController {

	private Rigidbody myBody;

	public Transform bullet_StartPoint;
	public GameObject bullet_Prefab;
	public ParticleSystem shootFX;

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

	

    private Animator shootSliderAnim;

	[HideInInspector]
	public bool canShoot;

	void Start () {
		myBody = GetComponent<Rigidbody> ();

		shootSliderAnim = GameObject.Find ("Fire Bar").GetComponent<Animator> ();

		GameObject.Find ("ShootBtn").GetComponent<Button> ().onClick.AddListener (ShootingControl);
		canShoot = true;

	}

	void Update () {
		ControlMovementWithKeyboard ();
		ChangeRotation ();
	}

	private void FixedUpdate() {
        
        MoveTank ();
        myBody.velocity = new Vector3((_joystick.Horizontal) * 3, myBody.velocity.y, _joystick.Vertical * 3);

    }

	private void MoveTank() {
		myBody.MovePosition (myBody.position + speed * Time.deltaTime);
    }

	void ControlMovementWithKeyboard() {
		
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			MoveLeft ();
		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			MoveRight ();
		}

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			MoveFast ();
		}

		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			MoveSlow ();
		}

		if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.A)) {
			MoveStraight ();
		}

		if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.D)) {
			MoveStraight ();
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.W)) {
			MoveNormal ();
		}

		if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {
			MoveNormal ();
		}
	}

	void ChangeRotation() {
		if (speed.x > 0) {
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.Euler (0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
			
		} else if (speed.x < 0) {
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.Euler (0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
			
		} else {
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.Euler (0f, 0f, 0f), Time.deltaTime * rotationSpeed);
		}
	}

	public void ShootingControl() {

		if (Time.timeScale != 0)
		{
			if (canShoot)
			{
				GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position,
					Quaternion.identity);
				bullet.GetComponent<BulletScript>().Move(2000f);
				shootFX.Play();


				canShoot = false;
				shootSliderAnim.Play("Fill");
			}
		}
		else
			canShoot = false;

	}

} // class





































