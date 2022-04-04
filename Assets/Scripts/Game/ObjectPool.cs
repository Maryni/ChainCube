using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public GameObject LastSpawnedGameObject => listCreated[listCreated.Count - 1];

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
       var findedObject =  listCreated.FirstOrDefault(x => !x.activeSelf && x.GetComponent<Cube>().Value <= 2);
       if (findedObject == null)
       {
           var newObject = Instantiate(prefab, transformPool);
           listCreated.Add(newObject);
           ChangeName();
           newObject.GetComponent<Cube>().ChangeCanCollision();
           return newObject;
       }
       ChangeName();
       return findedObject;
    }

    public void SetValuesOnAllCreatedPrefabs(int value)
    {
        for (int i = 0; i < listCreated.Count; i++)
        {
            if (listCreated[i].GetComponent<Cube>().Value == 0)
            {
                listCreated[i].GetComponent<Cube>().SetValue(value);
            }
        }
    }
    
    public void SetColorsOnAllCreatedPrefabs(Color32 color32)
    {
        for (int i = 0; i < listCreated.Count; i++)
        {
            if (listCreated[i].GetComponent<Cube>().Value <= 2)
            {
                listCreated[i].GetComponent<Cube>().SetColor32(color32); 
            }
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

    private void ChangeName()
    {
        for (int i = 0; i < listCreated.Count; i++)
        {
            listCreated[i].name = (i + 1).ToString();
        }
    }

    #endregion private functions
}
