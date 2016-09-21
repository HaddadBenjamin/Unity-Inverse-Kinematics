﻿using UnityEngine;

using System.Collections;

public class FootIKInformation : ABaseIKInformation
{
    #region Unity Behaviour
    void Awake()
    {
        base.Initialize();
    }
    #endregion

    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("Déplacez-vous sur les plateformes puis arrêtez-vous pour voir l'IK des pieds de votre personnage.");
        GUILayout.Label("<b>Pour se déplacer :</b> AWSD ou les flèches directionnelles.");
    }
    #endregion
}
