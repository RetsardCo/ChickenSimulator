using UnityEngine;


namespace com.nullproject.project1
{
    public class Comb : MonoBehaviour
    {
        public void Trigger(Season season)
        {
            // determine season then pass the amount
            
            AddingFood(0);
        }
        
        private void AddingFood(float amount)
        {
            
        }
        
        
        public bool isRedAndFull;

        public bool hasDriedBlood;
        
        public bool hasFrostbite;
        
        public bool hasPecks;

        public bool hasNicks;
    }
}