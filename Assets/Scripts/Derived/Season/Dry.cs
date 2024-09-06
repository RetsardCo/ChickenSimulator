using UnityEngine;


namespace com.nullproject.project1
{
    public class Dry : Season
    {
        public override float GetAdditionalConsumption()
        {
            return -10;
        }

        [ContextMenu("Enable UwU")]
        
        // Hit enable to run the commands per season
        public override void Enable()
        {
            base.Enable();
        }
    }
}