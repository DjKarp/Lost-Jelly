using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shaker : Blicker
{
    public override void Initialize(List<Jelly> jellies = null, Subject<bool> playGameSubject = null)
    {
        _jellies.Clear();
        _jellies = jellies;
        _timer = Random.Range(1.0f, 4.0f);

        base.Initialize(jellies, playGameSubject);
    }

    protected override void StartEffect()
    {
        _jelliesActive[Random.Range(0, _jelliesActive.Count)].transform.DOShakeRotation(0.5f);
    }
}
