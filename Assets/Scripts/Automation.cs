using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace com.nullproject.project1
{
    public partial class Automation : MonoBehaviour
    {
        private readonly int _magnitude = Animator.StringToHash("magnitude");

        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;

        [SerializeField] private Transform model;
        
        [Header("Distance of the New Destination"), SerializeField, Range(2, 5)]
        private int range;

        private Coroutine _behaviour;
        
        private void Start() => AutoRoam();

        private Vector3 RandomPosition(float radius)
        {
            var randDirection = Random.insideUnitSphere * radius;
            randDirection += agent.transform.position;

            NavMesh.SamplePosition(randDirection, out var navHit, radius, -1);
            return navHit.position;
        }

        public void AutoRoam()
        { 
            var radius = Random.Range(1, range);
            var destination = RandomPosition(radius);

            if (_behaviour != null) StopCoroutine(_behaviour);
            _behaviour = StartCoroutine(GoTo(destination));
        }

        private IEnumerator GoTo(Vector3 destination, bool auto = true)
        {
            agent.SetDestination(destination);
            model.LookAt(destination);

            while (agent.pathPending) yield return null;

            var remain = agent.remainingDistance;

            while (float.IsPositiveInfinity(remain) ||
                   remain - agent.stoppingDistance > float.Epsilon ||
                   agent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                remain = agent.remainingDistance;
                print($"{remain} - {agent.velocity.magnitude}");
                animator.SetFloat(_magnitude, agent.velocity.magnitude > 0 ? 1 : 0);

                yield return null;
            }

            animator.SetFloat(_magnitude, 0);

            if (!auto) yield break;
            
            yield return new WaitForSeconds(10);

            AutoRoam();
        }
    }
}