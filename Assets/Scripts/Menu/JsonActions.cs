using System.IO;
using UnityEngine;

public class JsonActions : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private string pathJson;
    [SerializeField] private DataLanguageList myDataLanguageList = new DataLanguageList();

    #endregion Inspector variables

    #region properties

    public DataLanguageList DataLanguageList => myDataLanguageList;

    #endregion properties
    
    #region Unity functions

    private void Awake()
    {
        if (File.Exists(pathJson))
        {
            var text = File.ReadAllText(pathJson);
            myDataLanguageList = JsonUtility.FromJson<DataLanguageList>(text);
        }
    }

    #endregion Unity functions
    
    #region public functions
    [ContextMenu("SaveToJson")]
    public void SaveToJson()
    {
      var jsonText =  JsonUtility.ToJson(myDataLanguageList);
      File.WriteAllText(pathJson,jsonText);
    }

    #endregion public functions
}

[System.Serializable]
public class DataLanguageList
{
    public DataLanguage[] DataLanguages;
}

[System.Serializable]
public class DataLanguage
{
    #region Inspector variables

    [SerializeField] private string enText;
    [SerializeField] private string ruText;

    #endregion Inspector variables

    #region properties

    public string RuText => ruText;
    public string EnText => enText;

    #endregion properties
    
}

