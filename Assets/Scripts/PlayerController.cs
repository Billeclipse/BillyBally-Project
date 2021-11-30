using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public float speed = 10f;
	public bool is_flat = true;
	public GameObject head;
	public GameManager game_manager_ref;
	public Camera mainCamera;
	public GameObject standardUI;
	public GameObject ZoomInMenu;
	public VirtualJoystick joystick;
	public AudioClip[] soundEffectsArray;	
	//public GameObject hand;

	private Rigidbody rigid_body;
	private bool can_move = true;
	private bool can_zoom = true;
	private float soundEffectsVolume = 1f;
	private Quaternion original_rotation;
	private float original_fov;
	private bool player_camera_zoomed_in = false;

	private void Start()
	{
		original_rotation = mainCamera.transform.rotation;
		original_fov = mainCamera.fieldOfView;
		rigid_body = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (can_move)
		{
			rigid_body.constraints = RigidbodyConstraints.None;
			//Accelerometer/Joystick Input Movement (Smartphone)

			Vector3 tilt;
			if (joystick && PlayerPrefsManager.GetJoystickEnabled())
			{
				tilt = Vector3.zero;

				tilt.x = joystick.Horizontal();
				tilt.z = joystick.Vertical();
			}
			else
			{
				tilt = Input.acceleration;

				if (is_flat)
				{
					tilt = Quaternion.Euler(90, 0, 0) * tilt;
				}
			}
			rigid_body.AddForce(tilt * speed);			
			//Debug.DrawRay(transform.position + Vector3.up, tilt, Color.green);

			//Keyboard Input Movement (PC)
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
			rigid_body.AddForce(movement * speed);
		}
		else
		{
			rigid_body.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	private void Update()
	{		
		if (head)
		{
			head.transform.position = this.transform.position;			
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Intel"))
		{
			if (soundEffectsArray.Length > 0)
			{
				AudioSource.PlayClipAtPoint(soundEffectsArray[0], transform.position, soundEffectsVolume);
			}			
			Destroy(other.gameObject);

		}else if (other.gameObject.CompareTag("EndLine"))
		{			
			if (game_manager_ref.GetIntelCount() <= 0)
			{
				if (soundEffectsArray.Length > 1)
				{
					AudioSource.PlayClipAtPoint(soundEffectsArray[1], transform.position, soundEffectsVolume);
				}
				game_manager_ref.EndLevel();
			}
		}
	}

	private void OnMouseDown()
	{
		ZoomIn();
	}

	private void ZoomIn()
	{
		if (mainCamera && can_zoom)
		{
			can_move = false;
			mainCamera.GetComponent<PlayerCamera>().followingPlayer = false;
			mainCamera.transform.rotation = Quaternion.identity;
			mainCamera.transform.position = this.transform.position;
			mainCamera.fieldOfView = 40f;
			standardUI.SetActive(false);
			ZoomInMenu.SetActive(true);
			player_camera_zoomed_in = true;
			Time.timeScale = 0f;
		}
	}

	public void ZoomOut()
	{
		if (mainCamera)
		{
			can_move = true;
			mainCamera.GetComponent<PlayerCamera>().followingPlayer = true;
			mainCamera.transform.rotation = original_rotation;
			mainCamera.fieldOfView = original_fov;
			standardUI.SetActive(true);
			ZoomInMenu.SetActive(false);
			player_camera_zoomed_in = false;
			Time.timeScale = 1f;
		}
	}	

	public void FreezeTimeAndMovement(bool boolean)
	{
		can_move = !boolean;
		if (boolean)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;
	}

	public void SetCanZoom(bool boolean)
	{
		can_zoom = boolean;
	}

	public void SetCanMove(bool boolean)
	{
		can_move = boolean;
	}

	public bool GetPlayerCameraZoomedIn()
	{
		return player_camera_zoomed_in;
	}
}
