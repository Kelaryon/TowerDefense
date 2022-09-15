using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly float panSpeed = 100f;
	private readonly float boarderThickness = 10f;
	private float zoomLevel;
	[SerializeField] private int[] x = new int[2];
	[SerializeField] private int[] z = new int[2];

	private float sensitivity;
	private float speed;
	private float maxZoom;
	private float zoomPosition;
	private Vector3 pos;

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
		CameraMovement();
		Zoom();
		transform.position = pos;

	}

	private void CameraMovement()
    {
		if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - boarderThickness)
		{
			pos.z += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= boarderThickness)
		{
			pos.z -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - boarderThickness)
		{
			pos.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= boarderThickness)
		{
			pos.x -= panSpeed * Time.deltaTime;
		}
		pos.x = Mathf.Clamp(pos.x, x[0], x[1]);
		pos.z = Mathf.Clamp(pos.z, z[0], z[1]);
	}

	private void Zoom()
    {
		float zoomInput = Input.GetAxis("Mouse ScrollWheel");
		float sum;
		sum = -zoomInput * sensitivity;
		zoomLevel += sum;
		zoomLevel = Mathf.Clamp(zoomLevel, 30f, maxZoom);
		zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed* Time.deltaTime);
		pos.y = zoomPosition;
	}
}
