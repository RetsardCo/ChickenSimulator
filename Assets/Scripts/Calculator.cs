using UnityEngine;


namespace com.nullproject.project1
{
    public static class Calculator
    {
        public static bool GetChanceBy(float min, float max, float probability)
        {
            return Random.Range(min, max) <= Mathf.Clamp(probability, min, max);
        }
        
        public static bool GetChanceBy(float probability)
        {
            return Random.Range(0, 100) <= Mathf.Clamp(probability, 0, 100);
        }
    }
}