using UnityEngine;
using System;

namespace dmdspirit.Tactical
{
    public class Vector3Tweener : Tweener
    {
        public Vector3 startValue;
        public Vector3 endValue;
        public Vector3 currentValue { get; protected set; }

        protected override void OnUpdate(object sender, EventArgs e)
        {
            currentValue = (endValue - startValue) * animationControl.currentValue + startValue;
        }
    }
}