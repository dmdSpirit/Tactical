using UnityEngine;
using System;

namespace dmdspirit.Tactical
{
    public abstract class Tweener : MonoBehaviour
    {
        public static float defaultDuration = 1f;
        public static Func<float, float, float, float> DefaultEquation = MathEquations.InOutQuad;
        public ValueAnimationControl animationControl;
        public bool destroyOnComplete = true;

        protected virtual void Awake()
        {
            animationControl = gameObject.AddComponent<ValueAnimationControl>();
        }

        protected virtual void OnEnable()
        {
            animationControl.updateEvent += OnUpdate;
            animationControl.completedEvent += OnComplete;
        }

        protected virtual void OnDisable()
        {
            animationControl.updateEvent -= OnUpdate;
            animationControl.completedEvent -= OnComplete;
        }

        protected virtual void OnDestroy()
        {
            if (animationControl != null)
                Destroy(animationControl);
        }

        protected abstract void OnUpdate(object sender, EventArgs e);

        protected virtual void OnComplete(object sender, EventArgs e)
        {
            if (destroyOnComplete)
                Destroy(this);
        }
    }
}