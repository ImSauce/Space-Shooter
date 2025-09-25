using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3.5f;
	[SerializeField]
	private GameObject _laserprefab;
	[SerializeField]
	private float _firerate = 0.5f;
	[SerializeField]
	private float _canfire = -1f;
	[SerializeField]
	private int _lives = 3;
	[SerializeField]
	private bool _istripleshotactive = false;
	[SerializeField]
	private GameObject _tripleshot;

	private SpawnManager _spawnmanager;

	void Start()
	{
		//take the current position = new position (0,0,0)
		transform.position = new Vector3(0, -3.5f, 0);

		_spawnmanager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		if (_spawnmanager == null)
		{
			Debug.LogError("The spawn manager is null");
		}
	}


	// Update is called once per frame
	void Update()
	{
		CalculateMovement();
		if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
		{
			FireLaser();
		}
	}

	void CalculateMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
		transform.Translate(direction * _speed * Time.deltaTime);

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

	}

	void FireLaser()
	{
		_canfire = Time.time + _firerate;

		if (_istripleshotactive == true)
		{
			Instantiate(_tripleshot, transform.position, Quaternion.identity);
		}
		else
		{
			Instantiate(_laserprefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
		}

	}

	public void Damage()
	{
		_lives -= 1;

		if (_lives < 1)
		{
			_spawnmanager.OnPlayerDeath();
			Destroy(this.gameObject);
		}
	}


	public void TripleShotActive()
	{
		_istripleshotactive = true;
		Debug.Log("true");
		StartCoroutine(TripleShotPowerDownRoutine());
	}
	IEnumerator TripleShotPowerDownRoutine()
	{
		while (_istripleshotactive == true)
		{
			yield return new WaitForSeconds(5.0f);
			_istripleshotactive = false;
		}
	}
	

}
