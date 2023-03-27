using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 日本語対応
public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    private float _maxGenerate = 3f;
    [SerializeField]
    private GameObject[] _item = null;
    [SerializeField]
    private Vector2 _rangeA = Vector2.zero;
    [SerializeField]
    private Vector2 _rangeB = Vector2.zero;
    [SerializeField]
    private float _keepOutRange = 1f;
    [SerializeField]
    private GameObject[] _keepOutObject = null;
    [SerializeField]
    private float _interval = 1f;
    private float _timer = 0f;

    private void Start()
    {
        for (int i = 0; i < _maxGenerate; i++)
        {
            ItemGenerate();
        }
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > _interval)
        {
            ItemGenerate();
            _timer = 0f;
        }
    }

    private void ItemGenerate()
    {
        if (_item != null && transform.childCount < _maxGenerate)
        {
            int n = Random.Range(0, _item.Length);
            float x = Random.Range(_rangeA.x, _rangeB.x);
            float y = Random.Range(_rangeA.y, _rangeB.y);
            Vector2 GeneratePoint = new Vector2(x, y);

            foreach (var point in _keepOutObject)
            {
                if (_keepOutObject != null && Vector2.Distance(point.transform.position, GeneratePoint) < _keepOutRange)
                {
                    return;
                }
            }
            Instantiate(_item[n], GeneratePoint, Quaternion.identity, transform);
        }
    }
}
