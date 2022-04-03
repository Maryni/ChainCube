using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private int countExamples;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform transformPool;
    [SerializeField] private List<GameObject> listCreated;

    #endregion Inspector variables

    #region properties

    public int ListCreatedLength => listCreated.Count;

    #endregion properties

    #region Unity functions

    private void Awake()
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

    public void SetValuesOnAllCreatedPrefabs(int value)
    {
        for (int i = 0; i < listCreated.Count; i++)
        {
            listCreated[i].GetComponent<Cube>().SetValue(value);
        }
    }
    
    public void SetColorsOnAllCreatedPrefabs(Color32 color32)
    {
        for (int i = 0; i < listCreated.Count; i++)
        {
            listCreated[i].GetComponent<Cube>().SetColor32(color32);
        }
    }

    #endregion public functions

    #region private functions

    private void Init()
    {
        for (int i = 0; i < countExamples; i++)
        {
            var currectObject = Instantiate(prefab, transformPool);
            listCreated.Add(currectObject);
            currectObject.SetActive(false);
            currectObject.GetComponent<Cube>().ChangeCanCollision();
            currectObject.transform.localPosition = Vector3.zero;
        }
    }

    #endregion private functions
}
