using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;

namespace WalkEnemy.Runtime
{
    public class Patrouille : MonoBehaviour
    {
        #region Publics

        

        #endregion


        #region Unity Api

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _etat = Etat.patrol;
        }

        private void Update()
        {

            if (rayCountalert >= alert) 
                _etat = Etat.targeting;
                
            DetectPlayer();
            
            switch (_etat)
            {
                 case Etat.patrol: MovePatrol(); 
                     break;
                case Etat.search:
                    if (targetpoint != null)
                    {
                        MoveToDetect(targetpoint.position);
                        Debug.Log("je me deplace vers " + targetpoint.position);
                    } 
                    break;
                case Etat.targeting:
                    if (player != null)
                    {
                        MoveEnemy(player);
                        Debug.Log("ALERT JOUEUR DETECTER je me deplace vers " + player);
                    }
                    break;
            }
            
        }

        #endregion


        #region Utils

        private void MovePatrol()
        {
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                indexWaypoint = (indexWaypoint + 1) % waypoints.Count;
                _agent.destination = waypoints[indexWaypoint].position;
            }
        }

        private void MoveToDetect(Vector3 destination)
        {
            _agent.destination = destination;
        }

        private void MoveEnemy(GameObject target)
        {
            _agent.destination = target.transform.position;
        }

        private void DetectPlayer()
        {
            float halfAngle = viewAngle * 0.5f;
            Vector3 origin = rayOrigin.position;
            Vector3 forward = transform.forward;
            
            bool sendThisFrame = false;

            for (int i = 0; i < rayCount; i++)
            {
                float t = i / (float)(rayCount - 1);
                float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
                Quaternion rotation = Quaternion.Euler(0,angle,0);
                Vector3 dir = rotation * forward;

                if (Physics.Raycast(origin, dir, out RaycastHit hitInfo, viewDistance , playerMask))
                {
                    if (!sendThisFrame)
                    {
                        rayCountalert++;
                        targetpoint = hitInfo.transform;
                        sendThisFrame = true;
                        _etat = Etat.search;
                        Debug.Log("detected");
                    }
                    Debug.DrawLine(origin, hitInfo.point, Color.red);
                }
                else
                {
                    Debug.DrawRay(origin, dir * viewDistance, Color.green);
                }
            }
        }
        
        

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates

        [SerializeField] private float viewAngle;
        [SerializeField] private float viewDistance;
        [SerializeField] private int rayCount;
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private GameObject player;
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private Transform rayOrigin;
        [SerializeField] private bool detected;
        private int rayCountalert;
        private NavMeshAgent _agent;
        private int _counterView;
        private int alert = 10;
        private int indexWaypoint = 0;
        private Transform targetpoint;
        private Etat _etat;

        private enum Etat
        {
            patrol,
            search,
            targeting
        }

        #endregion
    }
}
