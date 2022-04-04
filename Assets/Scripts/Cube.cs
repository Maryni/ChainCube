using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private Moving moving;
    [SerializeField] private int value;
    [SerializeField] private Color32 color;
    [SerializeField] private bool isControlling;
    [SerializeField] private bool canCollision;

    #endregion Inspector variables

    #region private variables
    
    private UnityAction<int> actionOnCollisionWithParams;
    private UnityAction actionOnCollisionWithoutParams;
    
    #endregion private variables

    #region properties

    public int Value => value;
    public Color32 Color32 => color;
    public bool IsControlling => isControlling;

    #endregion properties

    #region Unity functions

    private void OnEnable()
    {
       ChangeCanCollision();
       StopVelocity();
    }

    private void OnDisable()
    {
        ChangeCanCollision();
        StopVelocity();
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

    public void StopVelocity()
    {
        moving.ResetVelocity();
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

    public void ResetAllActions()
    {
        actionOnCollisionWithoutParams = null;
        actionOnCollisionWithParams = null;
    }
    
    public void ChangeCanCollision()
    {
        canCollision = !canCollision;
    }

    public void ChangeIsControlled()
    {
        isControlling = !isControlling;
    }

    public void Move()
    {
        if (isControlling)
        {
            moving.MoveForward();
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
