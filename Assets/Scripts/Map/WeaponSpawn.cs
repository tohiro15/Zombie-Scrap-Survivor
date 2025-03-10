using System.Collections;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponPrefabs;
    [SerializeField] private GameObject[] _spawnpoints;
    [SerializeField] private float _minSpawnTime = 10f;
    [SerializeField] private float _maxSpawnTime = 20f;
    [SerializeField] private float _dropChance = 50f;

    private void Start()
    {
        StartCoroutine(SpawnWeapons());
    }

    private IEnumerator SpawnWeapons()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

            Vector3 spawnPoint = GetRandomSpawnPoint();
            if (spawnPoint == Vector3.zero) continue;

            if (Random.Range(0f, 100f) <= _dropChance)
            {
                SpawnWeapon(spawnPoint);
                Debug.Log("Оружие появилось!");
            }
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // Перебираем все точки спауна
        foreach (GameObject spawnPointObject in _spawnpoints)
        {
            Collider[] colliders = Physics.OverlapSphere(spawnPointObject.transform.position, 0.5f);
            if (colliders.Length == 0)
            {
                return spawnPointObject.transform.position;
            }
        }

        return Vector3.zero;
    }

    private void SpawnWeapon(Vector3 spawnPoint)
    {
        GameObject weapon = _weaponPrefabs[Random.Range(0, _weaponPrefabs.Length)];

        Instantiate(weapon, spawnPoint, Quaternion.identity);
    }
}
