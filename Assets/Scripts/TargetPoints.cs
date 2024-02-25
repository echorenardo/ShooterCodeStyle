using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    private int _currentPoint = 0;

    public void ChangePoint()
    {
        _currentPoint++;

        if (_currentPoint >= _points.Count)
            _currentPoint = 0;
    }

    public Transform GetPoint() => _points[_currentPoint];
}