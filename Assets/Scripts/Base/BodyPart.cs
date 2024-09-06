using UnityEngine;


namespace com.nullproject.project1
{
    public abstract class BodyPart : MonoBehaviour
    {
        public Specification specification; 
        
        public virtual void Upgrade(LifeStage lifeStage) { }
        
        public virtual void AddConsumption(float amount) { }
        
        public virtual void Trigger(Season season) { }
    }
}