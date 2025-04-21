using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using R3;

// Ёффект блика на cолнце в уголке Jelly
public class Blicker : LevelEffects
{
    private List<Jelly> _jellies = new List<Jelly>();
    private List<Jelly> _jelliesActive = new List<Jelly>();

    public override void Initialize(List<Jelly> jellies = null, Subject<bool> playGameSubject = null)
    {
        _jellies.Clear();
        _jellies = jellies;
        _effectPrefab = Resources.Load<Blick>("Blick");
        _timer = Random.Range(1.0f, 4.0f);

        base.Initialize(jellies, playGameSubject);
    }

    private void Start()
    {
        EventManager.PlayerMove.AddListener(StartStopLevelEffect);
    }

    protected override void StartLevelEffects()
    {
        _jelliesActive.Clear();
        _jelliesActive = _jellies.Where(j => j.isActiveAndEnabled).ToList();

        if (_jelliesActive.Count > 0)
            GetBlick().transform.position = _jelliesActive[Random.Range(0, _jelliesActive.Count)].BlickPosition;
    }

    protected override Effect AddedNewLevelEffectsToPool(bool isPrewarm = false)
    {
        Blick blick = Instantiate(_effectPrefab, _effectParent) as Blick;
        blick.Initialize();
        blick.gameObject.SetActive(!isPrewarm);
        _effectPool.Add(blick);

        return _effectPool.LastOrDefault();
    }

    private Blick GetBlick()
    {
        Blick blick = null;
        foreach (Blick blick1 in _effectPool)
            if (!blick1.gameObject.activeSelf)            
            { 
                blick = blick1; 
                blick.gameObject.SetActive(true);
                break;
            }

        if (blick == null)
            blick = AddedNewLevelEffectsToPool() as Blick;
        else
            blick.Initialize();

        return blick;
    }    
}
