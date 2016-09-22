using UnityEngine;
using System.Collections;

public sealed class IKController : MonoBehaviour
{
    #region Fields
    public AIKSolver[] chains;
    private Transform myTransform = null;
    #endregion

    #region Properties
    public bool IsNegativeLocalScale
    {
        get
        {
            return 0 > this.myTransform.localScale.x;
        }
    }

    public Vector3 Position
    {
        get
        {
            return this.myTransform.localPosition;
        }
    }
    #endregion

    #region  Untiy Behaviour
    void Start()
    {
        this.myTransform = transform;
    }
    #endregion
}