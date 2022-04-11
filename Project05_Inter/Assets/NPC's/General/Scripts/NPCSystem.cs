using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSystem : NPC_StateMachine
{
    [Header("NPC Atributes")]
    public int interactionState;
    public GameObject interactionCanvas;
    public NPC_Configs configs;
    public NPC_CanvasManager canvasManager;
    public Rigidbody rb;
    private MeshRenderer myMaterial;
    public CharacterMovement movement;
    public CharacterController controller;

    [Header("NPC random Walk Configs")]
    public float speed = 20f;
    public float timeWaiting = 3f;
    public float timeWaitingRange = 1f;
    private float timeWaitingCounter;
    private bool isWaiting = true;

    public float maxDistanceRadiusToWalk = 4f;
    public Vector3 newPoint;
    public Vector3 direction;
    private Vector3 startPosition;

    [Header("Dialogue Atributtes")]
    public bool isTalking;

    //Others
    Vector3 atualPosition;
    Vector3 desiredPosition;
    float desiredMovementTime = 2f;
    bool IsInMovement;
    float enlapsedTime;
    float percentageComplete;
    bool isInDesiredPosition;

    private void Awake()
    {
        myMaterial = GetComponentInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        State = new NPC_Wait(this);

        startPosition = transform.position;
        myMaterial.material = configs.npcMaterial;

        isWaiting = true;
        isTalking = false;
        timeWaitingCounter = timeWaiting + Random.Range(-timeWaitingRange, timeWaitingRange);
    }

    private void Update()
    {
        desiredPosition.y = transform.position.y;
        NPCMovement2();
    }

    public void NPCStartMovement()
    {
        atualPosition = transform.position;

        IsInMovement = true;
        enlapsedTime = 0;
    }

    public void NPCCalculateMovement()
    {
        isInDesiredPosition = transform.position == desiredPosition;

        if (IsInMovement)
        {
            enlapsedTime += Time.deltaTime;
            percentageComplete = enlapsedTime / desiredMovementTime;

            controller.Move(Vector3.Lerp(atualPosition, desiredPosition, Mathf.SmoothStep(0, 1, percentageComplete)).normalized * speed * Time.deltaTime);
        }
    }

    public void NPCMovement2()
    {
        if (configs.walk)
        {
            if (!isTalking)
            {
                NPCCalculateMovement();

                if (isWaiting)
                    timeWaitingCounter -= Time.deltaTime;

                if (isInDesiredPosition)
                {
                    IsInMovement = false;
                    isWaiting = true;
                    //desiredPosition = Vector3.zero;
                }

                if (timeWaitingCounter <= 0)
                {
                    desiredPosition = RandomPoint();
                    timeWaitingCounter = timeWaiting + Random.Range(-timeWaitingRange, timeWaitingRange);

                    direction = newPoint - transform.position;
                    direction.Normalize();

                    isWaiting = false;
                    NPCStartMovement();
                }
            }
        }
    }

    public void NPCMovement()
    {
        if (configs.walk)
        {
            if (!isTalking)
            {
                if (isWaiting)
                    timeWaitingCounter -= Time.deltaTime;
                else
                    State.Move();

                if (timeWaitingCounter <= 0)
                {
                    newPoint = RandomPoint();
                    timeWaitingCounter = timeWaiting + Random.Range(-timeWaitingRange, timeWaitingRange);

                    direction = newPoint - transform.position;
                    direction.Normalize();

                    isWaiting = false;
                }

                if (Vector3.Distance(newPoint, transform.position) < .2f)
                {
                    Debug.Log(configs.npcName + "Ih, cheguei!");

                    if (isWaiting == false)
                        timeWaitingCounter = timeWaiting + Random.Range(-timeWaitingRange, timeWaitingRange);

                    direction = transform.position;
                    State.Stop();
                    isWaiting = true;
                }
            }
        }
    }

    public Vector3 RandomPoint()
    {
        Vector3 point = new Vector3(Random.Range(-maxDistanceRadiusToWalk, maxDistanceRadiusToWalk), 0f, Random.Range(-maxDistanceRadiusToWalk, maxDistanceRadiusToWalk)).normalized;
        point *= Random.Range(1f, maxDistanceRadiusToWalk);

        point += startPosition;

        return point;
    }

    public void StopMove()
    {
        if (isWaiting == false)
            timeWaitingCounter = timeWaiting + Random.Range(-timeWaitingRange, timeWaitingRange);

        State.Stop();
        isWaiting = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("NPC"))
        {
            StopMove();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startPosition, maxDistanceRadiusToWalk);
    }
}