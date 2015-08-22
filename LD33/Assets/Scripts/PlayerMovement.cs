using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
	[SerializeField]
	private float _moveSpeed;
	private Rigidbody2D _rb2d;
	// Use this for initialization
	void Start () {
		_rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = _rb2d.velocity;
		velocity.x = Input.GetAxisRaw("Horizontal")*_moveSpeed;
		velocity.y = Input.GetAxisRaw("Vertical")*_moveSpeed;
		_rb2d.velocity = velocity;
	}
}
