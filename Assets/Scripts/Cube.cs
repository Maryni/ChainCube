using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private int value;
    [SerializeField] private Color32 color;
    [SerializeField] private bool isControlling;
    [SerializeField] private bool canCollision;

    #endregion Inspector variables

    #region private variables

    private Moving moving;
    private UnityAction<int> actionOnCollisionWithParams;
    private UnityAction actionOnCollisionWithoutParams;
    
    #endregion private variables

    #region properties

    public int Value => value;
    public Color32 Color32 => color;
    public bool IsControlling => isControlling;
    public bool CanCollision => canCollision;

    #endregion properties

    #region Unity functions

    private void OnEnable()
    {
       ChangeCanCollision();
    }

    private void OnDisable()
    {
        ChangeCanCollision();
    }

    private void Start()
    {
        if (moving == null)
        {
            moving = GetComponent<Moving>();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Cube>())
        {
            var currentCube = collision.gameObject.GetComponent<Cube>();
            if (currentCube.Value == value && canCollision)
            {
                currentCube.gameObject.SetActive(false);
                moving.MoveUpper();
                actionOnCollisionWithParams?.Invoke(currentCube.value);
                actionOnCollisionWithoutParams?.Invoke();
            }
        }
    }

    #endregion Unity functions
    
    #region public functions

    public void SetValue(int value)
    {
        this.value = value;
    }

    public void SetColor32(Color32 color32)
    {
        color = color32;
        SetColorToMaterial();
    }

    public void ChangeCanCollision()
    {
        canCollision = !canCollision;
    }

    public void AddActionsOnCollisionWithParams(params UnityAction<int>[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnCollisionWithParams += actions[i];
        }
    }
    
    public void AddActionsOnCollisionWithoutParams(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnCollisionWithoutParams += actions[i];
        }
    }
    
    #endregion public functions

    #region private functions

    private void SetColorToMaterial()
    {
        GetComponent<Renderer>().material.color =color;
    }

    #endregion private functions
}
