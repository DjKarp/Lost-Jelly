using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using System.Linq;

// The effect of flying leaves on the level
public class FlyLeaves : LevelEffects
{
    private List<Sprite> _leavesSprite = new List<Sprite>();
    private float _startCreateX = 10.0f;
    private float _maxStartCreateY = 5.0f;

    protected Effect _effect;

    public override void Initialize(List<Jelly> jellies = null, Subject<bool> playGameSubject = null)
    {
        _leavesSprite.Clear();
        _leavesSprite.AddRange(Resources.LoadAll<Sprite>("Fly"));
        _effectPrefab = Resources.Load<Effect>("Leaves");
        _timer = Random.Range(1.0f, 8.0f);

        base.Initialize(jellies, playGameSubject);
    }
    private void Start()
    {
        EventManager.PlayerMove.AddListener(StartStopLevelEffect);
    }

    protected override void StartLevelEffects()
    {
        Leaves tempLeaves = null;

        foreach (Leaves leaves in _effectPool)
            if (!leaves.gameObject.activeSelf)
            { tempLeaves = leaves; break; }

        if (tempLeaves == null)
            AddedNewLevelEffectsToPool();
        else
            InitializeEffect(tempLeaves);
    }
    protected override void InitializeEffect(Effect effect)
    {
        base.InitializeEffect(effect);
        _effect = effect;
    }
    protected override void StartEffect()
    {
        _effect.Initialize(GetNewStartPositionForLeaves(), _leavesSprite[Random.Range(0, _leavesSprite.Count)]);
    }

    protected override Effect AddedNewLevelEffectsToPool(bool isPrewarm = false)
    {
        Leaves leaves = Instantiate(_effectPrefab, new Vector3(_startCreateX, 0.0f, 0.0f), Quaternion.identity, _effectParent) as Leaves;
        InitializeEffect(leaves);
        leaves.gameObject.SetActive(!isPrewarm);
        _effectPool.Add(leaves);
        return _effectPool.LastOrDefault();
    }    

    private Vector2 GetNewStartPositionForLeaves()
    {
        return new Vector2(_startCreateX, Random.Range(-1.0f * _maxStartCreateY, _maxStartCreateY));
    }    
}
