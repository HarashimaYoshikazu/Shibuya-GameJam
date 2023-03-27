using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 日本語対応
public class SmokeParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smoke = null;

    private void Start()
    {
        if(_smoke) _smoke.Play();
    }
}
