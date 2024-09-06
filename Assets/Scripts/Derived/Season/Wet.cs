using UnityEngine;


namespace com.nullproject.project1
{
    public class Wet : Season
    {
        public override float GetAdditionalConsumption()
        {
            return Random.Range(-5, 5);
        }
    }
}