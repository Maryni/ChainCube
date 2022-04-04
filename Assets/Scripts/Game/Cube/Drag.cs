using UnityEngine;
using UnityEngine.Events;

public class Drag : MonoBehaviour
{
    #region private variables

    private Camera cam;
    private Vector3 mouseOffset;
    private float mouseZCoordinate;
    private UnityAction actionOnMouseEndDrag;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        mouseZCoordinate = cam.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 resultVector = GetMouseWorldPosition() + mouseOffset;
        resultVector.y = gameObject.transform.position.y;
        transform.position = resultVector;
    }

    private void OnMouseUp()
    {
        var currentCube = gameObject.GetComponent<Cube>();
        if (currentCube.IsControlling)
        {
            currentCube.Move();
            currentCube.ChangeIsControlled(); 
            actionOnMouseEndDrag?.Invoke();
        }
    }

    #endregion Unity functions

    #region public functions

    public void SetActionOnMouseEndDrag(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnMouseEndDrag += actions[i];
        }
    }

    #endregion public functions
    
    #region private functions

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoordinate;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    #endregion private functions
}