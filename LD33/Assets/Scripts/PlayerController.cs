using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDamageable, IIceAffected
{
	public static readonly uint ATTACK_TYPE_FIRE = 0x1 << 1;
	public static readonly uint ATTACK_TYPE_WATER = 0x1 << 2;
	public static readonly uint ATTACK_TYPE_ICE = 0x1 << 3;
	public static readonly uint ATTACK_TYPE_ACID = 0x1 << 4;

	private uint _availableWeapons;
	private uint _selectedWeapon;
	private int _maxHealth;
	private int _health;
	private Rigidbody2D _rb2d;
	private float _lastHit;
	private int _numAvailableAttacks;
	private float _timeAbleToAttack;
	private uint _iceCollisions;
	private float _nextHumanSpawn;

	public uint AvailableAttacks {
		get { return _availableWeapons; }
		set {
			if((value & _availableWeapons)==0)
			{
				_numAvailableAttacks++;
			}
			_availableWeapons |= value;
			_selectedWeapon = value;
		}
	}

	[SerializeField]
	private float _acceleration;
	[SerializeField]
	private float _invincibilityPeriod;
	[SerializeField]
	private GameObject _fireBreath;
	[SerializeField]
	private GameObject _waterBreath;
	[SerializeField]
	private GameObject _iceBreath;
	[SerializeField]
	private GameObject _acidBreath;
	[SerializeField]
	private GameObject _human;
	[SerializeField]
	private GUIStyle _healthStyle;

	// Use this for initialization
	void Start () {
		_rb2d = gameObject.GetComponent<Rigidbody2D>();
		_availableWeapons = 0;
		_selectedWeapon = 0;
		_numAvailableAttacks = 0;
		_maxHealth = 100;
		_health = 100;
		_iceCollisions = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//spawn the humans!
		doSpawningHumansThing();

		//movement
		Vector2 force = new Vector2();
		force.x = Input.GetAxisRaw("Horizontal")*_acceleration;
		force.y = Input.GetAxisRaw("Vertical")*_acceleration;
		_rb2d.AddForce(force);

		//ice physics
		if (_iceCollisions > 0) _rb2d.drag = 1;
		else _rb2d.drag = 10;

		//switching weapons
		if(Input.GetButtonDown("Switch Weapons"))
		{
			if(_selectedWeapon == 0)
			{
				return;
			}
			do
			{
				_selectedWeapon <<= 1;
				if (_selectedWeapon > ATTACK_TYPE_ACID)
				{
					_selectedWeapon = 0x1 << 1;
				}
			} while ((_selectedWeapon & _availableWeapons) == 0);
		}
		if (Input.GetButtonDown("Fire Up"))
		{
			Attack(0);
		}
		if(Input.GetButtonDown("Fire Left"))
		{
			Attack(1);
		}
		if(Input.GetButtonDown("Fire Down"))
		{
			Attack(2);
		}
		if (Input.GetButtonDown("Fire Right"))
		{
			Attack(3);
		}
	}

	void OnGUI()
	{
		GUI.HorizontalScrollbar(new Rect(0, 0, Screen.width, 40), 0, _health, 0, 100);
		GUI.Label(new Rect(5, 0, Screen.width / 4 + 5, 20), "Health: " + _health + "/" + _maxHealth, _healthStyle);
	}

	private void Attack(int direction)
	{
		if(_timeAbleToAttack > Time.time)
		{
			return;
		}
		GameObject toCreate;
		switch(_selectedWeapon)
		{
			case 0: return;
			case 2: //TODO Fire attack
				toCreate = _fireBreath;
				break;
			case 4: //TODO Water attack
				toCreate = _waterBreath;
				break;
			case 8: //TODO Ice attack
				toCreate = _iceBreath;
				break;
			case 16: //TODO Acid attack
				toCreate = _acidBreath;
				break;
			default:
				return;
		}
		GameObject weapon = (GameObject) GameObject.Instantiate(toCreate, transform.position, Quaternion.Euler(0, 0, 90 * direction));
		weapon.transform.parent = transform;
		_timeAbleToAttack = toCreate.GetComponent<BreathWeapon>().Cooldown + Time.time;
	}

	private void die()
	{
		//http://forum.unity3d.com/threads/any-way-to-reset-the-scene.23986/
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void doDamage(int value)
	{
		if (value > 0)
		{
			if (_lastHit + _invincibilityPeriod < Time.time)
			{
				_lastHit = Time.time;
				_health -= value;
				if (_health < 0)
				{
					die();
				}
			}
		}
		else
		{
			_health += (_health + value > _maxHealth) ? _maxHealth - _health : _health + value;
		}
	}

	public int getHealth()
	{
		return _health;
	}

	public void incrementEntered()
	{
		_iceCollisions++;
	}

	public void decrementEntered()
	{
		_iceCollisions--;
	}

	private void doSpawningHumansThing()
	{
		if (_numAvailableAttacks == 0) return;
		Debug.Log("more attacks, pls.");
		if (Time.time > _nextHumanSpawn)
		{
			//important to check for 0 first
			_nextHumanSpawn = 4 / _numAvailableAttacks;
			_nextHumanSpawn *= (8000 - transform.position.x)/8000;
			_nextHumanSpawn += Time.time;
			//TODO spawn the human to the right
			Instantiate(_human, transform.position + (800 * Vector3.right), Quaternion.identity);
		}
	}
}
