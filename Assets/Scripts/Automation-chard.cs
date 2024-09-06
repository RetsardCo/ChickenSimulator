using UnityEngine;

namespace com.nullproject.project1
{
    public partial class Automation
    {
        /// <summary>
        /// Manually passing of a specific destination
        /// ex. Feeding area, Drinking area
        /// </summary>
        /// <param name="destination">The target location of the area</param>
        public void MoveTo(Vector3 destination)
        {
            if (_behaviour != null) StopCoroutine(_behaviour);
            agent.ResetPath();

            _behaviour = StartCoroutine(GoTo(destination, false));
        }
    }
}