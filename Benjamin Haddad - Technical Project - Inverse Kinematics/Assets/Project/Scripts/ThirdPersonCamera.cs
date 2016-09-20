using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
    #region Fields
    public float distanceAway;			
	public float distanceUp;			
	public float distanceRight;
    public float smoothSpeed;

    private Vector3 m_TargetPosition;

    private Transform target;
    private Transform myTransform;
    #endregion

    #region Unity Behaviour
    void Start()
    {
		this.target = GameObject.FindWithTag ("Player").transform;
        this.myTransform = transform;
    }
	
	void LateUpdate ()
	{
		m_TargetPosition = this.target.position + 
                           Vector3.up * this.distanceUp +
                           Vector3.right * this.distanceRight -
                           this.target.forward * this.distanceAway;

        this.myTransform.position = Vector3.Lerp(this.myTransform.position, m_TargetPosition, Time.deltaTime * this.smoothSpeed);

        this.myTransform.LookAt(target);
	}
    #endregion
}
