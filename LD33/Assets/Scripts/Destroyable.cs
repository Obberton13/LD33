using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[RequireComponent(typeof(BoxCollider2D))]
public class Destroyable : MonoBehaviour {

	[SerializeField]
	private List<uint> _destroyableBy;

	[SerializeField]
	private GameObject _createWhenDestroyed;

	void OnTriggerEnter2D(Collider2D other)
	{
		BreathWeapon bw;
		if ((bw = other.gameObject.GetComponent<BreathWeapon>())!=null)
		{
			if (_destroyableBy.Contains(bw.Type))
			{
				Destroy(gameObject);
				if (_createWhenDestroyed != null)
				{
					Vector3 position = transform.position;
					GameObject.Instantiate(_createWhenDestroyed, position, Quaternion.identity);
				}
			}
		}
	}
}
