using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeValueController : MonoBehaviour
{
   #region Inspector variables

   [SerializeField] private List<int> values = new List<int>();
   [SerializeField] private List<Color32> colors = new List<Color32>();

   #endregion Inspector variables

   #region public functions

   public int GetNextValue(int value)
   {
      int valueReturn = 0;
      for (int i = 0; i < values.Count; i++)
      {
         if (value == values[i])
         {
            valueReturn = values[i+1];  
            continue;
         }
      }

      if (valueReturn == 0)
      {
         Debug.LogError($"valueReturn = 0");
      }

      return valueReturn;
   }

   public int GetRandomBetweenTwoAndFour()
   {
      return values[Random.Range(0, 2)];
   }
   
   public Color32 GetNextColor(Color32 value)
   {
      Color32 colorReturn = new Color32();
      for (int i = 0; i < values.Count; i++)
      {
         if (value.r == colors[i].r &&
             value.g == colors[i].g &&
             value.b == colors[i].b &&
             value.a == colors[i].a)
         {
            colorReturn = colors[i+1];
            continue;
         }
      }
      
      if (value.r == 0 &&
          value.g == 0 &&
          value.b == 0 &&
          value.a == 0)
      {
         Debug.LogError($"colorReturn = 0, its just new Color32()");
      }

      return colorReturn;
   }

   public int GetValueByIndex(int index)
   {
      return values[index];
   }

   public Color32 GetColorByIndex(int index)
   {
      return colors[index];
   }

   #endregion public functions
}
