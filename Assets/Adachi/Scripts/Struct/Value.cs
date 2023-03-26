using System;
using UnityEngine;

[Serializable]
public struct Value<T>
{
    #region Properties

    public T MinValue => _minValue;
    public T MaxValue => _maxValue;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("è¨Ç≥Ç¢íl")]
    private T _minValue;

    [SerializeField]
    [Header("ëÂÇ´Ç¢íl")]
    private T _maxValue;

    #endregion

    #region Public Methods

    public void SetValue(T minValue, T maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }

    #endregion
}