using UnityEngine;

public class Moving : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private Rigidbody rig;

    #endregion Inspector variables

    #region Unity functions

    #endregion Unity functions

    #region public functions

    public void MoveForward()
    {
        rig.AddForce(Vector3.forward * speed,ForceMode.Impulse);
    }

    public void MoveUpper()
    {
        rig.velocity = Vector3.up * (speed / 4);
    }

    public void ResetVelocity()
    {
        rig.velocity = Vector3.one;
    }

    #endregion public functions
}
