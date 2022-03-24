using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float panSpeed = 50f;
	private float boarderThickness = 10f;
	private float zoomLevel;
	private float sensitivity;
	private float speed;
	private float maxZoom;
	float zoomPosition;
	Vector3 pos;

    private void Start()
    {
		zoomPosition = transform.position.y;
		zoomLevel = transform.position.y;
		pos = transform.position;
		sensitivity = 100;
		maxZoom = 100f;
		speed = 100f;
	}

    void Update()
	{
		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - boarderThickness)
		{
			pos.z += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= boarderThickness)
		{
			pos.z -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - boarderThickness)
		{
			pos.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= boarderThickness)
		{
			pos.x -= panSpeed * Time.deltaTime;
		}
		pos.x = Mathf.Clamp(pos.x, 30, 115);
		pos.z = Mathf.Clamp(pos.z, -95, -5);
		Zoom();
		transform.position = pos;

	}

	private void Zoom()
    {
		float zoomInput = Input.GetAxis("Mouse ScrollWheel");
		float sum;
		sum = -zoomInput * sensitivity;
		zoomLevel =zoomLevel + sum;
		zoomLevel = Mathf.Clamp(zoomLevel, 30f, maxZoom);
		zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed* Time.deltaTime);
		pos.y = zoomPosition;
	}
}
