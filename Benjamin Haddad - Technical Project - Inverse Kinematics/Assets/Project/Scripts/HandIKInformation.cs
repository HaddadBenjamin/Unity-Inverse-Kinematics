using UnityEngine;
using System.Collections;

public class HandIKInformation : ABaseIKInformation
{
    #region Unity Behaviour
    void Awake()
    {
        base.EnableIKListener += delegate (bool input)
        {
            GetComponent<HandIK>().EnableIK = input;
        };

        base.Initialize();
    }
    #endregion

    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("Activer / désactiver l'IK pour tenir l'objet en face de votre personnage.");
    }
    #endregion
}
