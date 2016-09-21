using UnityEngine;
using System.Collections;

public abstract class AIK : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool enableIK = false;
    #endregion

    #region Properties
    public bool EnableIK
    {
        get
        {
            return enableIK;
        }

        set
        {
            enableIK = value;
        }
    }
    #endregion
}
