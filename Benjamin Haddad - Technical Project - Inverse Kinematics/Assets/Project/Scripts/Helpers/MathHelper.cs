using UnityEngine;
using System.Collections;

public static class MathHelper
{
    /// <summary>
    /// Calcul l'angle entre 2 vecteurs compris entre [-180 et 180].
    /// Signed signifie la valeur de l'angle de retour (négatif ou positif) alors qu'unsigned signifie que l'angle sera forcèment positif.
    /// </summary>
    /// <returns></returns>
    public static float SignedAngle(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(Vector3.back, Vector3.Cross(a, b)));

        //Debug.LogFormat("Angle Signed : {0}", angle * sign);
        return angle * sign;
    }
}