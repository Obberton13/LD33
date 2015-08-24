using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class HumanAI : MonoBehaviour, IDamageable, IIceAffected{

	[SerializeField]
	private float _speed;
	[SerializeField]
	private int _health;
	[SerializeField]
	private int _damage = 2;
	[SerializeField]
	private float _invincibilityPeriod = .3f;
	[SerializeField]
	private float _defaultDrag;

	private float _lastHitTime;

	private PlayerController _player;
	private bool _collidingWithPlayer = false;
	private Rigidbody2D _rb2d;
	private BreathWeapon _collidingWithBreath;
	private uint _iceCollisions;


	// Use this for initialization
	void Start () {
		_rb2d = GetComponent<Rigidbody2D>();
		_health = 10;
		_player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 force = (-(Vector2)transform.position + (Vector2)_player.transform.position).normalized * _speed;
		_rb2d.AddForce(force);

		if(_collidingWithPlayer)
		{
			_player.doDamage(_damage);
		}
		if(_collidingWithBreath!=null)
		{
			doDamage(_collidingWithBreath.Damage);
		}

		if (_iceCollisions > 0) _rb2d.drag = 1;
		else _rb2d.drag = 10;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.GetComponent<PlayerController> ()!=null)
        {
			_collidingWithPlayer = true;
		}
		BreathWeapon collidingWith;
		if((collidingWith = other.GetComponent<BreathWeapon>())!=null)
		{
			_collidingWithBreath = collidingWith;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>()!=null)
		{
			_collidingWithPlayer = false;
			return;
		}
		if(other.GetComponent<BreathWeapon>()!=null)
		{
			_collidingWithBreath = null;
		}
	}

	public void doDamage(int amount)
	{
		if (_lastHitTime + _invincibilityPeriod < Time.time)
		{
			_health -= amount;
			_lastHitTime = Time.time;
			if (_health < 0)
			{
				Destroy(this.gameObject);
			}
		}
	}

	public void incrementEntered()
	{
		_iceCollisions++;
	}

	public void decrementEntered()
	{
		_iceCollisions--;
	}
}
