using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private AnimationCurve heightCurve;
    private float distance;
    protected override void Update()
    {
        base.Update();
    }
    protected override void FindEnemy(Vector3 target)
    {
        distance = Vector2.Distance(startTransform.position, target + new Vector3(0, 0.25f));
        float t = timeElapsed * speed / distance;

        Vector2 currentPoint = Vector2.Lerp(startTransform.position, target + new Vector3(0, 0.25f), t);
        float height = heightCurve.Evaluate(t) * distance;
        currentPoint.y += height;

        transform.right = ((Vector3)currentPoint - transform.position).normalized;

        transform.position = currentPoint;
    }
}
