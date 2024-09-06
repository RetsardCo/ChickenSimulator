using UnityEngine;


namespace com.nullproject.project1
{
    public sealed class Layer : Chicken
    {
        public bool isLaying;
        
        public override void GenerateEgg(int pendingEggsToLay)
        {
            if (pendingEggsToLay == 0)
            {
                isLaying = false;
                return;
            }

            if(pendingLayerEgg == 0)
            {
                isLaying = true;
                pendingLayerEgg = pendingEggsToLay;
            }

            if (!(Time.time > hatchTime)) return;
            
            laidEggs++;
            pendingLayerEgg--;
                
            bodyParts?.Find(bodyPart => bodyPart.specification == Specification.Crop)?.AddConsumption(5);
            hatchTime = Time.time + hatchDelayTime;
            
            print("Total laid eggs:" + laidEggs);
        }
    }
}