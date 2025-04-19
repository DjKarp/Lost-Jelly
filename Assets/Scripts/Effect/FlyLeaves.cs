using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using System.Linq;

public class FlyLeaves : MonoBehaviour
{
    public CompositeDisposable _disposables = new CompositeDisposable();

    private List<Sprite> _leavesSprite = new List<Sprite>();
    private Leaves _leavesPrefab;

    private List<Leaves> _leavesPool = new List<Leaves>();

    private float _startCreateX = 10.0f;
    private float _maxStartCreateY = 6.0f;
    private Transform _leavesParent;

    public void Initialize()
    {
        _leavesSprite.AddRange(Resources.LoadAll<Sprite>("Fly"));
        _leavesPrefab = Resources.Load<Leaves>("Leaves");

        _leavesParent = new GameObject("LeavesParent").transform;

        Observable
            .Interval(System.TimeSpan.FromSeconds(Random.Range(1.0f, 10.0f)))
            .Subscribe(_ => StartLeaves())
            .AddTo(_disposables);
    }

    private void StartLeaves()
    {
        Leaves tempLeaves = null;

        foreach (Leaves leaves in _leavesPool)
            if (!leaves.gameObject.activeSelf)
                tempLeaves = leaves;

        if (tempLeaves == null)
            AddedNewLeavesToPool();
        else
            InitializeLeaves(tempLeaves);
    }

    private Leaves AddedNewLeavesToPool()
    {
        Leaves leaves = Instantiate(_leavesPrefab, new Vector3(_startCreateX, 0.0f, 0.0f), Quaternion.identity, _leavesParent);
        InitializeLeaves(leaves);
        _leavesPool.Add(leaves);
        return _leavesPool.LastOrDefault();
    }

    private void InitializeLeaves(Leaves leaves)
    {
        leaves.gameObject.SetActive(true);
        leaves.Initialize(GetNewStartPositionForLeaves(), _leavesSprite[Random.Range(0, _leavesSprite.Count)]);
    }

    private Vector2 GetNewStartPositionForLeaves()
    {
        return new Vector2(_startCreateX, Random.Range(-1.0f * _maxStartCreateY, _maxStartCreateY));
    }
}
