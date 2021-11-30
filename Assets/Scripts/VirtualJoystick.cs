using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image backgroundImage;
	private Image joystickImage;
	private Vector3 inputVector;

	private void Start()
	{
		backgroundImage = GetComponent<Image>();
		joystickImage = transform.GetChild(0).GetComponent<Image>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
		{
			pos.x = (pos.x / backgroundImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / backgroundImage.rectTransform.sizeDelta.y);
			inputVector = new Vector3(pos.x * 2 + 1, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			// Move Joystick Image
			joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (backgroundImage.rectTransform.sizeDelta.x / 3), inputVector.y * (backgroundImage.rectTransform.sizeDelta.y / 3));
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnDrag(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		inputVector = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = Vector3.zero;
	}	

	public float Horizontal()
	{
		if(inputVector.x != 0)
		{
			return inputVector.x;
		}
		else
		{
			return Input.GetAxis("Horizontal");
		}
	}

	public float Vertical()
	{
		if (inputVector.y != 0)
		{
			return inputVector.y;
		}
		else
		{
			return Input.GetAxis("Vertical");
		}
	}
}
