using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private Player _player;

	[SerializeField]
	private float _speed = 4.0f;

	void Start()
	{
		transform.position = new Vector3(0, 7f, 0);
		_player = GameObject.Find("Player").GetComponent<Player>();
	}


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
		
		if (transform.position.y < -5f){
			float randomX = Random.Range(-8f,8f);
			transform.position = new Vector3(randomX,7,0);
		}
    }
	
	private void OnTriggerEnter2D(Collider2D other){
		Debug.Log("hit"+ other.transform.name);

		if (other.tag == "Laser"){
			Destroy(other.gameObject);

			if (_player != null)
			{
				_player.AddScore(10);
			}

			Destroy(this.gameObject);
		}
		if (other.tag == "Player"){
			Player player  = other.transform.GetComponent<Player>();
			if (player != null){
				player.Damage();
			}
			Destroy(this.gameObject,2.8f);
		}
	}
}
