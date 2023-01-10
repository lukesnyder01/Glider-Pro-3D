using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] spawnPrefab;

    [Range(0, 100)]
    public float spawnChance;

    public bool randomizeRotation = false;

    public bool makePrefabChild = false;


    void Start()
    {
        var coroutine = SpawnObject();
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(0.001f, 0.01f));

        if (Random.Range(0, 100) < spawnChance)
        {
            var selectedPrefab = spawnPrefab[(Random.Range(0, spawnPrefab.Length))];
            GameObject newSpawnedObject = Instantiate(selectedPrefab, transform.position, transform.rotation);

            if (randomizeRotation)
            {
                var rot = new Vector3(0, Random.Range(0, 360f), 0);
                newSpawnedObject.transform.rotation = Quaternion.Euler(rot);
            }

            if (!makePrefabChild)
            {
                transform.parent = null;
                newSpawnedObject.transform.parent = null;
            }
            else
            {
                newSpawnedObject.transform.parent = transform.parent;
                transform.parent = null;
            }
        }

        Destroy(gameObject);
    }
}
