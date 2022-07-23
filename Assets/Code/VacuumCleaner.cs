using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleaner : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private LayerMask _dustMask;

    [SerializeField]
    private Transform[] _spawnPoints;

    private Transform _currentTarget;

    private const float SideSize = 3.6f;

    private const float MinDistance = 0.001f;

    private void Start()
    {
        FindMovementTarget();
    }

    void Update()
    {
        Move();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SideSize * Vector2.one);
    }

    private void Move()
    {
        var distance = Vector2.Distance(transform.position, _currentTarget.position);

        if (distance < MinDistance)
        {
            LookForDust();
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, _speed);
    }

    private void LookForDust()
    {
        var results = new Collider2D[30];
        var resultCount = Physics2D.OverlapBoxNonAlloc(transform.position, Vector2.one * SideSize, 0f, results, _dustMask);

        if (resultCount > 0)
        {
            Clean(results);
        }

        FindMovementTarget();
    }

    private void Clean(Collider2D[] results)
    {
        foreach (var result in results)
        {
            if (result)
            {
                result.gameObject.SetActive(false);
            }
        }
    }

    private void FindMovementTarget()
    {
        _currentTarget = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}
