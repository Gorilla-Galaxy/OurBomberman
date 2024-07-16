using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private float destructionTime = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float itemSpawnChance = 0.2f;
    [SerializeField] private GameObject[] spawnableItens;
    
    private void Start() 
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy() {
        if (spawnableItens.Length > 0 && Random.value < itemSpawnChance) {
            int randomIndex = Random.Range(0, spawnableItens.Length);
            Instantiate(spawnableItens[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
