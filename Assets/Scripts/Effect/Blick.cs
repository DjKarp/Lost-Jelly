using UnityEngine;
public class Blick : Effect
{
    public override void Initialize(Vector2 vector2, Sprite sprite = null)
    {
        _timer = 2.0f;
        base.Initialize(vector2, sprite);
    }
    protected override void Action()
    {
        gameObject.SetActive(false);
    }
}
