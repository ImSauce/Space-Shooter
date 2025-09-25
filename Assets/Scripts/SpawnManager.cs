using UnityEngine;
using System.Collections;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
	private GameObject _enemyprefab;
	[SerializeField]
	private GameObject _tripleshotpowerupprefab;
	[SerializeField]
	private GameObject _enemycontainer;
	[SerializeField]
	private bool _stopspawning = false;

	void Start()
	{
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnTripleShotPowerUpRoutine());
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
	
	public void OnPlayerDeath()
	{
		_stopspawning = true;
	}
}
