using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _startPositionTR;

    public Transform StartPosition
    {
        get => _startPositionTR;
        set => _startPositionTR ??= value;
    }

    [SerializeField] private List<Jelly> _jellyListOnLevel = new List<Jelly>();

    public List<Jelly> JellyListOnLevel
    {
        get => _jellyListOnLevel;
        set => _jellyListOnLevel ??= value;
    }

    public void AddedOneJelly(Jelly jelly)
    {
        if (jelly != null && !_jellyListOnLevel.Contains(jelly)) _jellyListOnLevel.Add(jelly);
    }

}