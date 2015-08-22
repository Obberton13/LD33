using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private Transform _toFollow;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = _toFollow.transform.position;
		pos.y = 0;
		pos.z = -10;
		transform.position = pos;
	}
}
