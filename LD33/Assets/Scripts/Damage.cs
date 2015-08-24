using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class Damage : MonoBehaviour {
	[SerializeField]
	private int _damage;

	private List<IDamageable> _allCollisions = new List<IDamageable>();

	void Update()
	{
		_allCollisions.RemoveAll(item => item == null);
		foreach (IDamageable toDamage in _allCollisions)
		{
			toDamage.doDamage(_damage);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		PlayerController pc = other.GetComponent<PlayerController>();
		if(pc != null)
		{
			_allCollisions.Add(pc);
		}
		HumanAI human = other.GetComponent<HumanAI>();
		if(human != null)
		{
			_allCollisions.Add(human);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{

		PlayerController pc = other.GetComponent<PlayerController>();
		if (pc != null)
		{
			_allCollisions.Remove(pc);
		}
		HumanAI human = other.GetComponent<HumanAI>();
		if (human != null)
		{
			_allCollisions.Remove(human);
		}
	}
}
