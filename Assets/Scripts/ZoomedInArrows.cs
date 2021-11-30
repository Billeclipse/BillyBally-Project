using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomedInArrows : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public PlayerController MainPlayerController;
	public bool PositiveRotation;

	private IEnumerator coroutine;

	private void Start()
	{
		coroutine = RotatePlayerCamera(PositiveRotation);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		StartCoroutine(coroutine);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		StopCoroutine(coroutine);
	}

	IEnumerator RotatePlayerCamera(bool positive_side)
	{
		while (true)
		{
			if (MainPlayerController)
			{
				Camera mainCamera = MainPlayerController.mainCamera;
				bool player_camera_zoomed_in = MainPlayerController.GetPlayerCameraZoomedIn();

				if (mainCamera && player_camera_zoomed_in)
				{
					float new_rotation_x = mainCamera.transform.eulerAngles.x;
					float new_rotation_y = mainCamera.transform.eulerAngles.y;
					float new_rotation_z = mainCamera.transform.eulerAngles.z;
					if (positive_side)
						new_rotation_y++;
					else
						new_rotation_y--;

					mainCamera.transform.rotation = Quaternion.Euler(new_rotation_x, new_rotation_y, new_rotation_z);
				}
			}
			yield return null;
		}			
	}	
}
