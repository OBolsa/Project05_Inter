using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Card_StateMachineR
{
    [Header("Card Config")]
    public CardConfig Config;
    public CardUiElements UIElements;
    public int CardImportance;

    [Header("External Scripts")]
    public GameObject cardModel;
    public Collider cardCollider;
    public IHighlightSelection highlightSelection;
    public bool IsMouseOver => MouseSelector.HitCollider() == cardCollider;

    [Header("Card System Atributtes")]
    public Vector3 startPosition;
    public Quaternion startRotation;
    private Vector3 atualPosition;
    private Quaternion atualRotation;
    private Vector3 beforeSelectPosition;
    private Quaternion beforeSelectRotation;
    public bool Select { get; set; }
    public bool UnSelect { get; set; }
    public bool IsSelected { get; set; }


    [Header("Card Movement Atributes")]
    public float desiredMovementTime;
    public bool IsInMovement { get; private set; }
    private float enlapsedTime;
    private float percentageComplete;
    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private void Awake()
    {
        highlightSelection = GetComponent<IHighlightSelection>();
    }

    private void Start()
    {
        SetState(new Card_StartR(this));
        highlightSelection.HighlightInitialize(transform);
    }

    private void Update()
    {
        if (Select)
        {
            beforeSelectPosition = transform.position;
            beforeSelectRotation = transform.rotation;

            Select = false;
        }
        if (UnSelect)
        {
            StartCardMovement(beforeSelectPosition, beforeSelectRotation, 0.3f);
            UnSelect = false;
        }
    }

    public void CardHighlight()
    {
        if (IsMouseOver && !IsSelected)
            highlightSelection.OnHighlight(transform);
        else
            highlightSelection.OnDehighlight(transform);
    }

    public void StartCardMovement(Vector3 pos, Quaternion rotation, float movementTime)
    {
        atualPosition = transform.position;
        atualRotation = transform.rotation;

        desiredPosition = pos;
        desiredRotation = rotation;
        desiredMovementTime = movementTime;

        IsInMovement = true;
        enlapsedTime = 0;
    }

    public void MovementCard()
    {
        bool isInDesiredPosition = transform.position == desiredPosition;

        if (IsInMovement)
        {
            enlapsedTime += Time.deltaTime;
            percentageComplete = enlapsedTime / desiredMovementTime;

            transform.SetPositionAndRotation(Vector3.Lerp(atualPosition, desiredPosition, Mathf.SmoothStep(0, 1, percentageComplete)), Quaternion.Lerp(atualRotation, desiredRotation, Mathf.SmoothStep(0, 1, percentageComplete)));

            if (isInDesiredPosition)
            {
                IsInMovement = false;
                desiredPosition = Vector3.zero;
                desiredRotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }

    public void AddCardImportance(int value)
    {
        CardImportance += value;
    }

    public void ResetCardImportance()
    {
        CardImportance = 0;
    }
}