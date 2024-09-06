using UnityEngine;


namespace com.nullproject.project1
{
    public class ChickenManager : MonoBehaviour
    {
        public Classification classification;

        public Chicken chicken;

        public Crop crop;
        
        #region Delegates and Events

        public delegate void OnGenerateEggHandled(int pendingEggsToLay);

        public OnGenerateEggHandled onGenerateEgg;

        #endregion

        private void Start()
        {
            onGenerateEgg += chicken.GenerateEgg;
            crop = chicken.bodyParts?.Find(bodyPart => bodyPart.specification == Specification.Crop) as Crop;
        }

        private void Update()
        {
            Live();
        }

        private void Live()
        {
            switch (chicken.lifeStage)
            {
                case LifeStage.Egg:
                    //chicken.bodyParts?.ForEach(bodyPart => bodyPart.AddConsumption(0));
                    break;
                case LifeStage.Hatchling:
                    //chicken.bodyParts?.ForEach(bodyPart => bodyPart.AddConsumption(.0001f));
                    break;
                case LifeStage.Chick:
                    //chicken.bodyParts?.ForEach(bodyPart => bodyPart.AddConsumption(.001f));
                    break;
                case LifeStage.Pullet:
                    //chicken.bodyParts?.ForEach(bodyPart => bodyPart.AddConsumption(.01f));
                    break;
                case LifeStage.Hen:
                    //chicken.bodyParts?.ForEach(bodyPart => bodyPart.AddConsumption(.1f));
                    
                    var pendingEggsToLay = Calculator.GetChanceBy(20) ? 2 : 1;
                    
                    onGenerateEgg?.Invoke(crop.IsFull ? pendingEggsToLay : 0);
                    break;
            }
        }
    }
}