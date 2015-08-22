using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

	private Camera _camera;
	[SerializeField]
	private Transform _toFollow;
	
	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = _toFollow.transform.position;
		pos.y = 0;
		pos.z = -10;
		float halfWidth = _camera.orthographicSize * _camera.aspect;
		pos.x = (pos.x<=(halfWidth-4096f)?halfWidth-4096:(pos.x>4096- halfWidth)?4096-halfWidth:pos.x);
		transform.position = pos;
	}
}
