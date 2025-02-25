using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _liveTime;

    private Coroutine _coroutine;
    private float _endTime;

    public event Action<Enemy> Deactivated;

    private void Init(Vector3 direction)
    {
        _endTime = Time.time + _liveTime;

        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void StartLiveCycle(Vector3 direction)
    {
        Init(direction);

        _coroutine = StartCoroutine(TravellingTime());
    }

    private IEnumerator TravellingTime()
    {
        while (Time.time < _endTime)
        {
            Displacement();

            yield return null;
        }

        Deactivate();
    } 

    private void Displacement()
    {
        transform.Translate(transform.forward * Time.deltaTime * _speed, Space.World);
    }

    private void Deactivate()
    {
        StopCoroutine(_coroutine);

        Deactivated?.Invoke(this);
    }
}