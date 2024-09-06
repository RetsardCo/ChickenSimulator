using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.nullproject.project1
{
    public partial class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject chick;

        [SerializeField] private int count;

        public List<Automation> chicks;

        private void Start()
        {
            chicks = new List<Automation>();
            _ = StartCoroutine(Release());
        }

        private IEnumerator Release()
        {
            if (count == 0) yield break;

            while (count > 0)
            {
                var chickObj = Instantiate(chick);
                chicks.Add(chickObj.GetComponent<Automation>());
                count--;

                yield return new WaitForSeconds(1);
            }
        }

        public void Feed()
        {
            var destination = Vector3.zero;
            
            foreach (var automation in chicks)
            {
                automation.MoveTo(destination);
                destination.z += 0.1f;
            }
        }

        public void Stop()
        {
            foreach (var automation in chicks)
            {
                automation.AutoRoam();
            }
        }
    }
}