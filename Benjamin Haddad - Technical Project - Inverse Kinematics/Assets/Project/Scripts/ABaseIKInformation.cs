﻿using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public abstract class ABaseIKInformation : MonoBehaviour
{
    #region Fields
    private AIK AIK;
    private ThirdPersonCamera myCamera;
    private bool enableIK = true;
    private float cameraDistanceAway;
    private float cameraDistanceUp;
    private float cameraDistanceRight;
    private float lightIntensity;
    [SerializeField]
    private Light directionalLight;
    private readonly Rect beginArea = new Rect(10.0f, 10.0f, 560.0f, 250.0f);
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
            this.AIK.EnableIK = 
                 enableIK = 
                 value;
        }
    }

    public float CameraDistanceAway
    {
        get
        {
            return cameraDistanceAway;
        }

        set
        {
            float tmp = CameraDistanceAway;

            cameraDistanceAway = value;

            if (tmp != cameraDistanceAway)
                this.myCamera.distanceAway = cameraDistanceAway;
        }
    }

    public float CameraDistanceUp
    {
        get
        {
            return cameraDistanceUp;
        }

        set
        {
            float tmp = cameraDistanceUp;

            cameraDistanceUp = value;

            if (tmp != cameraDistanceUp)
                this.myCamera.distanceUp = cameraDistanceUp;
        }
    }

    public float LightIntensity
    {
        get
        {
            return lightIntensity;
        }

        set
        {
            float tmp = lightIntensity;

            lightIntensity = value;

            if (lightIntensity != tmp)
                directionalLight.intensity = lightIntensity;
        }
    }

    public float CameraDistanceRight
    {
        get
        {
            return cameraDistanceRight;
        }

        set
        {
            float tmp = cameraDistanceRight;

            cameraDistanceRight = value;

            if (tmp != cameraDistanceRight)
                this.myCamera.distanceRight = cameraDistanceRight;
        }
    }
    #endregion

    #region Virtual Behaviour
    protected virtual void GUIInformation()
    {
        this.EnableIK = GUILayout.Toggle(this.EnableIK, "Activer / désactiver l'IK.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Charger une scène.");
        if (GUILayout.Button("La scène courante"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUILayout.Button("IK des pieds"))
            SceneManager.LoadScene("Foot IK");
        if (GUILayout.Button("IK des mains"))
            SceneManager.LoadScene("Hand IK");
        if (GUILayout.Button("IK du regard"))
            SceneManager.LoadScene("Look IK");
        GUILayout.EndHorizontal();

        GUILayout.Label("Afin de rendre plus visible le comportement des animations IK, vous pouvez modifier les paramètres ci-dessous.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Distance de la caméra par rapport au joueur.");
        this.CameraDistanceAway = GUILayout.HorizontalSlider(this.CameraDistanceAway, -20.0f, 20.0f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Distance up de la caméra par rapport au joueur.");
        this.CameraDistanceUp = GUILayout.HorizontalSlider(this.CameraDistanceUp, 0.05f, 15.0f, GUILayout.Width(100.0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Distance right de la caméra par rapport au joueur.");
        this.CameraDistanceRight = GUILayout.HorizontalSlider(this.CameraDistanceRight, -10.0f, 10.0f, GUILayout.Width(100.0f));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Intensité de la lumière.");
        this.LightIntensity = GUILayout.HorizontalSlider(this.LightIntensity, 0.0f, 2.0f);
        GUILayout.EndHorizontal();
    } 
    #endregion

    #region Unity Behaviour

    void OnGUI()
    {
        GUILayout.BeginArea(this.beginArea, GUIContent.none);

        GUI.color = Color.green;
        this.GUIInformation();

        GUILayout.EndArea();
    }
    #endregion

    #region Protected Behaviour
    protected void Initialize()
    {
        this.AIK = GetComponent<AIK>();

        this.EnableIK = this.AIK.EnableIK;

        this.myCamera = Camera.main.GetComponent<ThirdPersonCamera>();

        this.CameraDistanceAway = this.myCamera.distanceAway;
        this.CameraDistanceUp = this.myCamera.distanceUp;
        this.CameraDistanceRight = this.myCamera.distanceRight;
        this.LightIntensity = this.directionalLight.intensity;
    }
    #endregion
}
