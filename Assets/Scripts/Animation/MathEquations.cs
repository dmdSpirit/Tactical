using UnityEngine;
using System;

namespace dmdspirit.Tactical
{
    public static class MathEquations
    {
        public static float Linear(float start, float end, float value)
        {
            return (end - start) * value + start;
        }

        public static float Spring(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
            return start + (end - start) * value;
        }

        public static float InQuad(float start, float end, float value)
        {
            end -= start;
            return end * value * value + start;
        }

        public static float OutQuad(float start, float end, float value)
        {
            end -= start;
            return -end * value * (value - 2) + start;
        }

        public static float InOutQuad(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1)
                return end / 2 * value * value + start;
            value--;
            return -end / 2 * (value * (value - 2) - 1) + start;
        }

        public static float InCubic(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value + start;
        }

        public static float OutCubic(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value + 1) + start;
        }

        public static float InOutCubic(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1)
                return end / 2 * value * value * value + start;
            value -= 2;
            return end / 2 * (value * value * value + 2) + start;
        }

        public static float InQuart(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value + start;
        }

        public static float OutQuart(float start, float end, float value)
        {
            value--;
            end -= start;
            return -end * (value * value * value * value - 1) + start;
        }

        public static float InOutQuart(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1)
                return end / 2 * value * value * value * value + start;
            value -= 2;
            return -end / 2 * (value * value * value * value - 2) + start;
        }

        public static float InQuint(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value * value + start;
        }

        public static float OutQuint(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value * value * value + 1) + start;
        }

        public static float InOutQuint(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1)
                return end / 2 * value * value * value * value * value + start;
            value -= 2;
            return end / 2 * (value * value * value * value * value + 2) + start;
        }

        public static float InSine(float start, float end, float value)
        {
            end -= start;
            return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
        }

        public static float OutSine(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
        }

        public static float InOutSine(float start, float end, float value)
        {
            end -= start;
            return -end / 2 * (Mathf.Cos(Mathf.PI * value / 1) - 1) + start;
        }

        public static float InExpo(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Pow(2, 10 * (value / 1 - 1)) + start;
        }

        public static float OutExpo(float start, float end, float value)
        {
            end -= start;
            return end * (-Mathf.Pow(2, -10 * value / 1) + 1) + start;
        }

        public static float InOutExpo(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1)
                return end / 2 * Mathf.Pow(2, 10 * (value - 1)) + start;
            value--;
            return end / 2 * (-Mathf.Pow(2, -10 * value) + 2) + start;
        }

        public static float InCirc(float start, float end, float value)
        {
            end -= start;
            return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
        }

        public static float OutCirc(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * Mathf.Sqrt(1 - value * value) + start;
        }

        public static float InOutCirc(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1)
                return -end / 2 * (Mathf.Sqrt(1 - value * value) - 1) + start;
            value -= 2;
            return end / 2 * (Mathf.Sqrt(1 - value * value) + 1) + start;
        }

        public static float InBounce(float start, float end, float value)
        {
            end -= start;
            float d = 1f;
            return end - OutBounce(0, end, d - value) + start;
        }

        public static float OutBounce(float start, float end, float value)
        {
            value /= 1f;
            end -= start;
            if (value < (1 / 2.75f))
            {
                return end * (7.5625f * value * value) + start;
            }
            else if (value < (2 / 2.75f))
            {
                value -= (1.5f / 2.75f);
                return end * (7.5625f * value * value + .75f) + start;
            }
            else if (value < (2.5 / 2.75))
            {
                value -= (2.25f / 2.75f);
                return end * (7.5625f * value * value + .9375f) + start;
            }
            else
            {
                value -= (2.625f / 2.75f);
                return end * (7.5625f * value * value + .984375f) + start;
            }
        }

        public static float InOutBounce(float start, float end, float value)
        {
            end -= start;
            float d = 1f;
            if (value < d / 2)
                return InBounce(0, end, value * 2) * 0.5f + start;
            else
                return OutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
        }

        public static float InBack(float start, float end, float value)
        {
            end -= start;
            value /= 1;
            float s = 1.70158f;
            return end * value * value * ((s + 1) * value - s) + start;
        }

        public static float OutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value = (value / 1) - 1;
            return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
        }

        public static float InOutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value /= .5f;
            if ((value) < 1)
            {
                s *= (1.525f);
                return end / 2 * (value * value * (((s) + 1) * value - s)) + start;
            }
            value -= 2;
            s *= (1.525f);
            return end / 2 * ((value) * value * (((s) + 1) * value + s) + 2) + start;
        }

        public static float Punch(float amplitude, float value)
        {
            float s = 9;
            if (value == 0)
                return 0;
            if (value == 1)
                return 0;
            float period = 1 * 0.3f;
            s = period / (2 * Mathf.PI) * Mathf.Asin(0);
            return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
        }

        public static float InElastic(float start, float end, float value)
        {
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s = 0;
            float a = 0;

            if (value == 0)
                return start;
            if ((value /= d) == 1)
                return start + end;
            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        }

        public static float OutElastic(float start, float end, float value)
        {
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s = 0;
            float a = 0;

            if (value == 0)
                return start;
            if ((value /= d) == 1)
                return start + end;
            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
        }

        public static float InOutElastic(float start, float end, float value)
        {
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s = 0;
            float a = 0;

            if (value == 0)
                return start;
            if ((value /= d / 2) == 2)
                return start + end;
            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            if (value < 1)
                return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
            return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
        }
    }
}