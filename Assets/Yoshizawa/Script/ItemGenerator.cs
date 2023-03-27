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
    private Transform _rangeA ;
    [SerializeField]
    private Transform _rangeB ;
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
        if(GameManager.Instance.IsPause)
        {
            return;
        }
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
            float x = Random.Range(_rangeA.position.x, _rangeB.position.x);
            float y = Random.Range(_rangeA.position.y, _rangeB.position.y);
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
