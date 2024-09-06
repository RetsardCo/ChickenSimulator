using System.Collections.Generic;
using UnityEngine;


namespace com.nullproject.project1
{
    public abstract class Season : MonoBehaviour
    {
        public List<ChickenManager> chickensManagers;
        
        public virtual void Enable()
        {
            chickensManagers.ForEach(chickensManager =>
            {
                chickensManager.chicken.onTrigger?.Invoke(this);
            });
        }

        public virtual float GetAdditionalConsumption() { return 0; }
    }
}