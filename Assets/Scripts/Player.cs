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
	
	private SpawnManager _spawnmanager;
	
   void Start() {
		//take the current position = new position (0,0,0)
		transform.position = new Vector3(0, -3.5f, 0);
		
		_spawnmanager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		if (_spawnmanager == null){
			Debug.LogError("The spawn manager is null");
		}
	}
	

	// Update is called once per frame
	void Update()
	{ 
		CalculateMovement();
		if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire){
		FireLaser();
		}
	}
	
	void CalculateMovement(){
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
		transform.Translate(direction * _speed * Time.deltaTime);
		
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f,0),0);
	
	}
	
	void FireLaser(){
		_canfire = Time.time + _firerate;
		Instantiate(_laserprefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
	}

	public void Damage(){
		_lives -=1;
		
		if (_lives < 1){
			_spawnmanager.OnPlayerDeath();
			Destroy(this.gameObject);
		}
	}
	

}
