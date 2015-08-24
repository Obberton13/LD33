using UnityEngine;
using System.Collections;

public class BreathWeapon : MonoBehaviour
{
	[SerializeField]
	private int _damage;
	[SerializeField]
	private float _cooldown;
	[SerializeField]
	private float _destroyAfterSeconds;
	[SerializeField]
	private uint _type;

	public int Damage { get { return _damage; } }
	
	public float Cooldown { get { return _cooldown; } }

	public uint Type { get { return _type; } }

	void Start()
	{
		Destroy(gameObject, _destroyAfterSeconds);
	}
}
