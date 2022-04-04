using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoader : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private List<Text> buttonsText;
    [SerializeField] private bool ruText;

    #endregion Inspector variables

    #region private variables

    private JsonActions jsonActions;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
        if (jsonActions == null)
        {
            jsonActions = GetComponent<JsonActions>();
        }
        
        SetText();
    }

    #endregion Unity functions

    #region public functions

    public void SetTextRu()
    {
        ruText = true;
        SetText();
    }

    public void SetTextEng()
    {
        ruText = false;
        SetText();
    }

    #endregion public functions
    
    #region private functions

    private void SetText()
    {
        for (int i = 0; i < buttonsText.Count; i++)
        {
            if (ruText)
            {
                buttonsText[i].text = jsonActions.DataLanguageList.DataLanguages[i].RuText;
            }
            else
            {
                buttonsText[i].text = jsonActions.DataLanguageList.DataLanguages[i].EnText;
            }
            
        }
    }

    #endregion private functions
}
