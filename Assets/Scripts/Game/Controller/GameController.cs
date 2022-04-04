using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private Transform playerTransform;

    #endregion Inspector variables
    
    #region private variables

    private ObjectPool objectPool;
    private CubeValueController cubeValueController;
    private Text scoreText;
    private Coroutine controlledObjectCoroutine;
    private Cube currentGameObject;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
       Init(); 
       SetValuesToSpawnedEnemy();
       SetActionToActions();
       SetObjectAsControlledObject();
    }

    #endregion Unity functions

    #region private functions

    private void Init()
    {
        if (cubeValueController == null)
        {
            cubeValueController = GetComponent<CubeValueController>();
        }

        if (objectPool == null)
        {
            objectPool = GetComponent<ObjectPool>();
        }

        if (scoreText == null)
        {
            scoreText = FindObjectOfType<Text>();
        }
    }

    private void SetValuesToSpawnedEnemy()
    {
        objectPool.SetValuesOnAllCreatedPrefabs(cubeValueController.GetValueByIndex(0));
        objectPool.SetColorsOnAllCreatedPrefabs(cubeValueController.GetColorByIndex(0));
    }

    private void SetActionToActions()
    {
        List<Cube> tempList = new List<Cube>();
        for (int i = 0; i < objectPool.ListCreatedLength; i++)
        {
            var currentCube = objectPool.GetObject().GetComponent<Cube>();
            currentCube.gameObject.SetActive(true); 
            tempList.Add(currentCube);
            SetActionToCube(currentCube);
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].gameObject.SetActive(false);
        }
        tempList.Clear();
    }

    private void AddScore(int value)
    {
        var currentTextInt = int.Parse(scoreText.text);
        currentTextInt += value;
        scoreText.text = currentTextInt.ToString();
    }

    private void SetObjectAsControlledObject()
    {
        currentGameObject = objectPool.GetObject().GetComponent<Cube>();
        SetValuesToSpawnedEnemy();
        currentGameObject.ChangeIsControlled();
        currentGameObject.transform.position = playerTransform.position;
        currentGameObject.gameObject.SetActive(true);
        //currentGameObject.StopVelocity();
    }

    private void EnableSetObjectOnDefaultPlaceCoroutine()
    {
        if (controlledObjectCoroutine == null)
        {
            controlledObjectCoroutine = StartCoroutine(SetObjectAsControlledObjectWithDelay());
        }
    }
    
    private IEnumerator SetObjectAsControlledObjectWithDelay()
    {
        yield return new WaitForSeconds(0.3f);
        SetObjectAsControlledObject();
        SetActionToCube(currentGameObject);
        StopCoroutine(controlledObjectCoroutine);
        controlledObjectCoroutine = null;
    }

    private void SetActionToCube(Cube currentCube)
    {
        currentCube.ResetAllActions();
        currentCube.AddActionsOnCollisionWithParams(AddScore);
        currentCube.AddActionsOnCollisionWithoutParams(
            () => currentCube.SetValue(cubeValueController.GetNextValue(currentCube.Value)),
            () => currentCube.SetColor32(cubeValueController.GetNextColor(currentCube.Color32)));
        currentCube.GetComponent<Drag>().SetActionOnMouseEndDrag(EnableSetObjectOnDefaultPlaceCoroutine);
    }

    #endregion private functions
}
