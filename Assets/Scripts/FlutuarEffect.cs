using UnityEngine;

public class FlutuarEffect : MonoBehaviour
{
	public float amplitude = 0.5f;
	public float speed = 1f;

	private Vector3 initialPosition;
	
	void Start()
	{
		initialPosition = transform.position;
	}

	void Update()
	{
		float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

		transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
	}
}