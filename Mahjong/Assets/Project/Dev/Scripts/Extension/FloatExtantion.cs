using UnityEngine;

namespace Project.Dev.Scripts.Extension
{
    public static class FloatExtension
    {
        public static bool AlmostEquals(float target, float value)
        {
            return Mathf.Abs(target - value) < Mathf.Epsilon;
        }
    }
}