using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCreator : MonoBehaviour
{
    [SerializeField]
    private float _spawnInterval = 1f;
    
    [SerializeField]
    private float _spawnChance = 27f;

    [SerializeField]
    private Transform[] _spawnPoints;

    [SerializeField]
    private GameObject _dustPrefab;

    private const float HalfSideSize = 1.8f;

    private void Start()
    {
        StartCoroutine(_SpawnRoutine());
    }

    private void SpawnDust()
    {
        if (Random.Range(0f, 100f) > _spawnChance)
        {
            return;
        }
        
        var dust = Instantiate(_dustPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)], false);
        dust.transform.localPosition = new Vector2(Random.Range(-HalfSideSize, HalfSideSize), Random.Range(-HalfSideSize, HalfSideSize));
    }

    private IEnumerator _SpawnRoutine()
    {
        SpawnDust();
        yield return new WaitForSeconds(_spawnInterval);
        StartCoroutine(_SpawnRoutine());
    }
}
