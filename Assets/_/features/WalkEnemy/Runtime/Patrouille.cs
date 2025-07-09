using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;

namespace WalkEnemy.Runtime
{
    public class Patrouille : MonoBehaviour
    {
        #region Publics

        public bool m_visionActive;
        public int m_rayCountalert;

        #endregion


        #region Unity Api

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _etat = Etat.patrol;
            alert = rayCount /5;
            _speedAlert = _agent.speed + 1;
        }

        private void Update()
        {
            if (m_visionActive)
                DetectPlayer();
            
            switch (_etat)
            {
                 case Etat.patrol: MovePatrol(); 
                     break;
                 case Etat.search:
                    if (isSearching)
                    {
                        MoveToDetect(lastSeenPosition);

                        float distance = Vector3.Distance(transform.position, lastSeenPosition);
                        if (distance < _agent.stoppingDistance + 0.2f)
                        {
                            isSearching = false;
                            isWaiting = true;
                            waitTimer = waitTimeAfterSearch;
                            //_agent.isStopped = true;
                            //m_rayCountalert = 0;
                            initialYRotation = _agent.transform.rotation.eulerAngles.y;
                           // Debug.Log("Joueur perdu, retour à la patrouille");
                        }
                    }
                    else if (isWaiting)
                    {
                            waitTimer -= Time.deltaTime;
                            float osciliation = Mathf.Sin(Time.time * lookAroundSpeed) * lookAroundAngle;
                            Vector3 newRotation = new Vector3(0,initialYRotation + osciliation,0);
                            transform.eulerAngles = newRotation;
                            if (waitTimer <= 0)
                            {
                                isWaiting = false;
                                //_agent.isStopped = false;
                                m_rayCountalert = 0;
                                _etat = Etat.patrol;
                                
                               // Debug.Log("rien trouver");
                                isSearching = false;
                            }
                    }
                    break;
                case Etat.targeting:
                    if (player != null)
                    {
                        MoveEnemy();
                       // Debug.Log("ALERT JOUEUR DETECTER je me deplace vers " + player);
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

        private void MoveEnemy()
        {
            _agent.destination = player.transform.position;
            _agent.speed = _speedAlert;
            animator.SetBool("IsRunning",true);
        }

        private void DetectPlayer()
        {
            float halfAngle = viewAngle * 0.5f;
            Vector3 origin = rayOrigin.position;
            Vector3 forward = transform.forward;

            int rayThatHitPlayer = 0;
            bool alreadySwitchStat = false;

            for (int i = 0; i < rayCount; i++)
            {
                float t = i / (float)(rayCount - 1);
                float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
                Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.up);
                Vector3 dir = rotation * forward;
                
                int mask = playerMask | obstacleMask;

                if (Physics.Raycast(origin, dir, out RaycastHit hitInfo, viewDistance , mask))
                {
                    if (((1 << hitInfo.transform.gameObject.layer) & playerMask) != 0)
                    {
                        lastSeenPosition = hitInfo.point;
                        rayThatHitPlayer++;
                        //m_rayCountalert++;
                        isSearching = true;
                        _etat = Etat.search;
                        //Debug.Log("detected");
                    }
                    Debug.DrawLine(origin, hitInfo.point, Color.red);
                }
                else
                {
                    Debug.DrawRay(origin, dir * viewDistance, Color.green);
                }
                m_rayCountalert = rayThatHitPlayer;

                if (m_rayCountalert >= alert)
                {
                    _etat = Etat.targeting;
                    isSearching = false;
                    isWaiting = false;
                }
                else if (m_rayCountalert > 0)
                {
                    //lastSeenPosition = hitInfo.point;
                    isSearching = true;
                    _etat = Etat.search;
                }
            }

        }
        
        

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates

        [SerializeField] private Collider zoneDetector;
        [SerializeField] private float viewAngle;
        [SerializeField] private float viewDistance;
        [SerializeField] private int rayCount;
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private GameObject player;
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private Transform rayOrigin;
        [SerializeField] private bool detected;
        [SerializeField] private float waitTimeAfterSearch = 2f;
        [SerializeField] private float lookAroundSpeed = 1.5f;
        [SerializeField] private float lookAroundAngle = 60f;
        [SerializeField] private Patrouille thisPatrouille;
        [SerializeField] private Detector detector;
        [SerializeField] private Animator animator;
        private float initialYRotation;
        private float waitTimer = 0f;
        private bool isWaiting = false;
        private NavMeshAgent _agent;
        private int alert;
        private int indexWaypoint = 0;
        private Transform targetpoint;
        private Etat _etat;
        private Vector3 lastSeenPosition;
        private bool isSearching = false;
        private float _speedAlert;

        private enum Etat
        {
            patrol,
            search,
            targeting
        }

        #endregion
    }
}
