using UnityEngine;
using System.Collections;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
	private GameObject _enemyprefab;
	[SerializeField]
	private GameObject _tripleshotpowerupprefab;
	[SerializeField]
	private GameObject _speedpowerupprefab;
	[SerializeField]
	private GameObject[] _powerups;
	[SerializeField]
	private GameObject _enemycontainer;
	[SerializeField]
	private bool _stopspawning = false;

	void Start()
	{
		StartCoroutine(SpawnEnemyRoutine());
		//StartCoroutine(SpawnTripleShotPowerUpRoutine());
		//StartCoroutine(SpawnSpeedPowerUpRoutine());
		StartCoroutine(SpawnPowerUpRoutine());
	}
	
	IEnumerator SpawnEnemyRoutine(){
		while (_stopspawning == false){
			Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
			GameObject newEnemy = Instantiate(_enemyprefab, posToSpawn, Quaternion.identity);
			newEnemy.transform.parent = _enemycontainer.transform;
			yield return new WaitForSeconds(5.0f);
		}
	}
	
	IEnumerator SpawnTripleShotPowerUpRoutine(){
		while (_stopspawning == false){
			Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
			Instantiate(_tripleshotpowerupprefab, posToSpawn, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(3,8));
		}
	}
	IEnumerator SpawnSpeedPowerUpRoutine(){
		while (_stopspawning == false){
			Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
			Instantiate(_speedpowerupprefab, posToSpawn, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(3,8));
		}
	}
	IEnumerator SpawnPowerUpRoutine(){
		while (_stopspawning == false){
			Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
			int randomPowerUp = Random.Range(0,3);
			Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(3f,8f));
		}
	}
	
	public void OnPlayerDeath()
	{
		_stopspawning = true;
	}
}
