using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
	[SerializeField]
	private float _acceleration;

	private Rigidbody2D _rb2d;
	// Use this for initialization
	void Start () {
		_rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 force = new Vector2();
		force.x = Input.GetAxisRaw("Horizontal")*_acceleration;
		force.y = Input.GetAxisRaw("Vertical")*_acceleration;
		_rb2d.AddForce(force);
	}
}
