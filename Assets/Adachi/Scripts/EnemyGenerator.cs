using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    [Header("一般人")]
    private EnemyBase[] _enemy = null;

    [SerializeField]
    [Header("クールタイム")]
    private Value<float> _coolTime;

    [SerializeField]
    [Header("2点の場所")]
    private Value<EnemyTrash>[] _enemyTrash;

    public bool IsEnd = false;

    public async void Generate()
    {
        while (true)
        {
            if(IsEnd)
            {
                break;
            }
            var coolTime = Calculator.RandomTime(_coolTime.MinValue, _coolTime.MaxValue);
            await UniTask.Delay(TimeSpan.FromSeconds(coolTime));

            var enemy = Instantiate(_enemy[Calculator.RandomIndex(_enemy)]);
            enemy.transform.SetParent(transform);
            var twoPos = _enemyTrash[Calculator.RandomIndex(_enemyTrash)];
            var randomNum = Calculator.RandomNumber();
            if(randomNum == 0) enemy.Init(twoPos.MinValue.transform, twoPos.MaxValue.transform);
            else enemy.Init(twoPos.MaxValue.transform, twoPos.MinValue.transform);
            enemy.transform.position = enemy.TwoPos.MinValue.position;
            enemy.OnMove();
        }
    }
}
