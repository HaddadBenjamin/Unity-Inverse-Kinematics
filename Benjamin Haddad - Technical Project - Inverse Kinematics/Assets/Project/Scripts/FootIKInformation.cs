using UnityEngine;

using System.Collections;


public class FootIKInformation : ABaseIKInformation
{
    #region Unity Behaviour
    void Awake()
    {
        base.EnableIKListener += delegate (bool input)
        {
            GetComponent<FootIK>().EnableIK = input;
        };

        base.Initialize();
    }
    #endregion

    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("<b>Pour se déplacer :</b> AWSD ou les flèches directionnelles.");
        GUILayout.Label("Déplacez-vous sur les plateformes pour voir l'IK des pieds de votre personnage.");
    }
    #endregion
}
