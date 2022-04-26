using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Hero;
using UnityEngine;


namespace BehaviourTree.Conditionals
{
    public class CanSeePlayer : Conditional
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The searching mask")]
        public SharedLayerMask SearchMask;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The obstacles mask")]
        public SharedLayerMask ObstacleMask;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The enemy transform")]
        public SharedTransform EnemyTransform;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The time between raycasts")]
        public readonly SharedFloat CheckTime = 0.15f;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The field of view angle of the agent (in degrees)")]
        public readonly SharedFloat FieldOfViewAngle = 90;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The distance that the agent can see")]
        public readonly SharedFloat ViewDistance = 1000;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The object that is within sight")]
        public SharedGameObject ReturnedObject;

        public readonly SharedInt ObstaclesCount;

        private WaitForSecondsRealtime _checkSeconds;
        private readonly Collider[] _hits = new Collider[3];
        private readonly RaycastHit[] _obstacleHits = new RaycastHit[1];


        public override void OnAwake()
        {
            base.OnAwake();
            _checkSeconds = new WaitForSecondsRealtime(CheckTime.Value);
            StartCoroutine(Check());
        }

        public override TaskStatus OnUpdate()
        {
            if (ReturnedObject.Value != null)
                return TaskStatus.Success;
            
            return TaskStatus.Failure;
        }

        private IEnumerator Check()
        {
            int hitsCount;
            while (gameObject.activeSelf)
            {
                hitsCount = Hits();
                ReturnedObject.Value = hitsCount > 0 ? PlayerObject(hitsCount) : null;
               

                yield return _checkSeconds;
            }
        }

        private GameObject PlayerObject(int hitsCount)
        {
            for (int i = 0; i < hitsCount; i++)
            {
                if (_hits[i].TryGetComponent(out HeroStateMachine hero) && IsInFieldOfView(hero.transform))
                {
                    return _hits[i].gameObject;
                    
                }
            }

            return null;
        }

        private int Hits() => 
            Physics.OverlapSphereNonAlloc(EnemyTransform.Value.position, ViewDistance.Value, _hits, SearchMask.Value);

        private bool IsInFieldOfView(Transform hitedObject)
        {
            var direction = (hitedObject.position - EnemyTransform.Value.position).normalized;
            direction.y = 0;
            var angle = Vector3.Angle(direction, EnemyTransform.Value.forward);
            if (direction.magnitude < ViewDistance.Value && angle < FieldOfViewAngle.Value * 0.5f)
                return LineOfSight(hitedObject);

            return false;
        }

        private bool LineOfSight(Transform hitedObject)
        {
            ObstaclesCount.Value = Physics.RaycastNonAlloc(
                EnemyTransform.Value.position, 
                (hitedObject.position - EnemyTransform.Value.position).normalized,
                _obstacleHits,
                ViewDistance.Value,
                ObstacleMask.Value);
            Debug.DrawRay(EnemyTransform.Value.position, (hitedObject.position - EnemyTransform.Value.position).normalized * ViewDistance.Value, Color.red, 1f);
            return ObstaclesCount.Value <= 0;
        }
#if UNITY_EDITOR    
        public override void OnDrawGizmos()
        {
            var oldColor = UnityEditor.Handles.color;
            var color = Color.yellow;
            color.a = 0.1f;
            UnityEditor.Handles.color = color;

            var halfFOV = FieldOfViewAngle.Value * 0.5f;
            var beginDirection = Quaternion.AngleAxis(-halfFOV, Vector3.up) * Owner.transform.forward;
            UnityEditor.Handles.DrawSolidArc(Owner.transform.position, Owner.transform.up, beginDirection, FieldOfViewAngle.Value, ViewDistance.Value);

            UnityEditor.Handles.color = oldColor;
        }
#endif
    }
}
