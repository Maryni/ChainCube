using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIActions : MonoBehaviour
{
 #region public functions

 public void Exit()
 {
  Application.Quit();
 }

 public void LoadLevel()
 {
  SceneManager.LoadSceneAsync(1);
 }

 #endregion public functions
}
