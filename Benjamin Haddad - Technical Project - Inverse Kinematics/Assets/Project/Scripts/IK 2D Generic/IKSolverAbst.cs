using UnityEngine;
using System.Collections;

public abstract class IKSolverAbst : MonoBehaviour
{
    public abstract void UpdateIK();

    /// <summary>
    /// Cette méthode ne sert à rien !!!
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float ClampAngle(float angle, float min, float max)
    {
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// Permet Vector3.Angle avec un signe : [0-180] -> [-180 - 180]
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float SignedAngle(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b); // angle compris entre 0 et 180
        float sign = Mathf.Sign(Vector3.Dot(Vector3.back, Vector3.Cross(a, b)));

        Debug.LogFormat("Angle Signed : {0}", angle * sign);
        return angle * sign;
    }
}