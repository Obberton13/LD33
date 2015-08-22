using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class HumanAI : MonoBehaviour {
	[SerializeField]
	private float _humanSpeed;
	[SerializeField]
	private Transform _targetLocation;
	private Rigidbody2D _rb2d;
	// Use this for initialization
	void Start () {
		_rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 force = (-(Vector2)transform.position + (Vector2)_targetLocation.position).normalized * _humanSpeed;
		_rb2d.AddForce(force);
	}
}
