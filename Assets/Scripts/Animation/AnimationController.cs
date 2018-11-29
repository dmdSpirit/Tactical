using UnityEngine;
using System.Collections;
using System;

namespace dmdspirit.Tactical
{
    public class AnimationController : MonoSingleton<AnimationController>
    {
        public IEnumerator MoveAnimation(Transform objectToMove, Vector3 from, Vector3 to, float speed, Action OnAnimationFinished )
        {
            float step = (speed / (from - to).magnitude) * Time.fixedDeltaTime;
            float t = 0;
            while (t <= 1)
            {
                t += step;
                objectToMove.position = Vector3.Lerp(from, to, t);
                yield return new WaitForFixedUpdate();
            }
            objectToMove.position = to;
            if (OnAnimationFinished != null)
                OnAnimationFinished();
        }
    }
}