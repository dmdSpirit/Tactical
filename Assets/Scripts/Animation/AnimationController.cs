using UnityEngine;
using System.Collections;
using System;

namespace dmdspirit.Tactical
{
    public class AnimationController : MonoSingleton<AnimationController>
    {
        public float animationSpeed = 1;

        public IEnumerator WalkHorizontally(Transform objectToMove, Vector3 from, Vector3 to, float speed, Action OnAnimationFinished )
        {
            float step = (speed / (from - to).magnitude) * Time.fixedDeltaTime * animationSpeed;
            float t = 0;
            while (t <= 1)
            {
                t += step;
                // TODO: (dmdspirit) Check do I have to do this every time, or Lerp will handle t>1 ok.
                t = Math.Min(t, 1);
                objectToMove.position = Vector3.Lerp(from, to, t);
                yield return new WaitForFixedUpdate();
            }
            objectToMove.position = to;
            if (OnAnimationFinished != null)
                OnAnimationFinished();
        }
    }
}