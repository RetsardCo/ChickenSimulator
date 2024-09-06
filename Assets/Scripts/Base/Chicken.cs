using System.Collections.Generic;
using UnityEngine;


namespace com.nullproject.project1
{
    public abstract class Chicken : MonoBehaviour
    {
        [Header("Chicken Identification")]
        
        public int tagNumber;
        
        public string petName;
        
        public LifeStage lifeStage;

        public Breed breed;

        [Header("Chicken Specification")]
        
        public int age;
        
        [Header("Chicken Body Parts")]
        
        public List<BodyPart> bodyParts;
        
        [Header("Hatching Mechanics (delay in seconds)")]

        public float hatchDelayTime;

        public float laidEggs;

        protected float hatchTime;

        [SerializeField] protected int pendingLayerEgg;
        
        #region Protected Virtuals

        protected virtual void Awake() { }

        protected virtual void Start()
        {
            bodyParts?.ForEach(bodyPart => onTrigger += bodyPart.Trigger);
        }

        protected virtual void Update()
        {
            ModifyAge();
        }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }

        #endregion
        
        #region Delegates

        public delegate void OnTriggerHandled(Season season);

        #endregion
        
        #region Events

        public OnTriggerHandled onTrigger;

        #endregion
        
        #region Methods

        public virtual void GenerateEgg(int pendingEggsToLay) { }

        private void ModifyAge()
        {
            var lifeStageByAge = GetLifeStageByAge(age);

            if (!lifeStageByAge.isMet) return;
            
            lifeStage = lifeStageByAge.lifeStage;
            bodyParts?.ForEach(bodyPart => bodyPart.Upgrade(lifeStageByAge.lifeStage));
            
            // Modify look or model
        }

        private (bool isMet, LifeStage lifeStage) GetLifeStageByAge(int value)
        {
            var stage = value switch
            {
                (int)LifeStage.Egg => LifeStage.Egg,
                (int)LifeStage.Hatchling => LifeStage.Hatchling,
                (int)LifeStage.Chick => LifeStage.Chick,
                (int)LifeStage.Pullet => LifeStage.Pullet,
                (int)LifeStage.Hen => LifeStage.Hen,
                _ => lifeStage
            };
            
            return (stage != lifeStage, stage);
        }
        
        #endregion
    }
}
