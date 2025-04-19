using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using R3;

public class Blicker : MonoBehaviour
{
    public CompositeDisposable _disposables = new CompositeDisposable();

    private List<Jelly> _jellies;
    private List<Jelly> _jelliesActive = new List<Jelly>();
    private Blick _blickPrefab;
    private List<Blick> _blicks = new List<Blick>();
    private Transform _blicksParent;

    public void Initialize(List<Jelly> jellies)
    {
        _jellies = jellies;
        _blickPrefab = Resources.Load<Blick>("Blick");
        _blicksParent = new GameObject("BlicksParent").transform;

        Observable
            .Interval(System.TimeSpan.FromSeconds(Random.Range(1.0f, 4.0f)))
            .Subscribe(_ => ShowBlick())
            .AddTo(_disposables);
    }

    private void ShowBlick()
    {
        _jelliesActive.Clear();
        _jelliesActive = _jellies.Where(j => j.gameObject.activeSelf).ToList();

        Jelly jelly = _jelliesActive[Random.Range(0, _jelliesActive.Count)];

        if (jelly != null)
        {
            GetBlick().transform.position = jelly.BlickPosition;
        }
    }

    private Blick CreateNewBlick()
    {
        Blick blick = Instantiate(_blickPrefab, _blicksParent);
        blick.Initialize();
        _blicks.Add(blick);

        return _blicks.LastOrDefault();
    }

    private Blick GetBlick()
    {
        Blick blick = null;
        foreach (Blick blick1 in _blicks)
            if (!blick1.gameObject.activeSelf)
            { blick = blick1; blick.gameObject.SetActive(true); }

        if (blick == null)
            blick = CreateNewBlick();
        else
            blick.Initialize();

        return blick;
    }

    private void OnDisable()
    {
        _disposables.Dispose();
    }
}
