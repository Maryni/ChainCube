using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform transformPool;
    [SerializeField] private List<GameObject> listCreated;
    [SerializeField] private int countExamples;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        Init();
    }

    #endregion Unity functions
    
    #region public functions

    public GameObject GetObject()
    {
       var findedObject =  listCreated.FirstOrDefault(x => !x.activeSelf);
       if (findedObject == null)
       {
           var newObject = Instantiate(prefab, transformPool);
           listCreated.Add(newObject);
           return newObject;
       }

       return findedObject;
    }

    #endregion public functions

    #region private functions

    private void Init()
    {
        for (int i = 0; i < countExamples; i++)
        {
            var currectObject = Instantiate(prefab, transformPool);
            listCreated.Add(currectObject);
        }
    }

    #endregion private functions
}
