using UnityEngine;
using System;
  
[RequireComponent(typeof(Animator))]  
[RequireComponent(typeof(CapsuleCollider))]  
public class FootIK : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool enableIK = false;
    public LayerMask raycastLayerMask;

    public Transform leftFoot;
    public Vector3 leftFootOffset;
    private float leftFootWeight = 0.0f;

    public Transform rightFoot;
    public Vector3 rightFootOffset;
    private float rightFootWeight = 0.0f;
	
	private Vector3 leftFootPosition;
	private Vector3 rightFootPosition;

    protected Animator myAnimator;

    private CapsuleCollider myCapsuleCollider;
	private Vector3 newColliderCenter;
	private float newColliderHeight;

    private float transformWeigth = 1.0f;
    private const float moveColliderCenterAndHeightSpeedAndTransformWeightSpeed = 10;
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

    #region Unity Behaviour
    void Start () 
	{
		myAnimator = GetComponent<Animator>();
		myCapsuleCollider = GetComponent<CapsuleCollider>();

		newColliderCenter = myCapsuleCollider.center;
		newColliderHeight = myCapsuleCollider.height;
	}

    void Update()
    {
        if (EnableIK)
        {
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle"))
                IdleUpdateCollider();
            else if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk") || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
                WalkRunUpdateCollider();
        }
        else
        {
            myCapsuleCollider.center = new Vector3(0, Mathf.Lerp(myCapsuleCollider.center.y, newColliderCenter.y, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed), 0);
            myCapsuleCollider.height = Mathf.Lerp(myCapsuleCollider.height, newColliderHeight, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);
        }
    }

    void OnAnimatorIK(int layerIndex)
	{	
		if (myAnimator)
		{	
			if (EnableIK)
			{

                if (transformWeigth != 1.0f)
                {
                    transformWeigth = Mathf.Lerp(transformWeigth, 1.0f, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);

                    if (transformWeigth >= 0.99)
                        transformWeigth = 1.0f;
                }
                if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle") &&
                                myCapsuleCollider.attachedRigidbody.velocity.magnitude < 0.1f)
                {
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, transformWeigth);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, transformWeigth);
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, transformWeigth);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, transformWeigth);

                    IdleIK();
                }
                else if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk") ||
                                     myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
                {
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, transformWeigth * leftFootWeight);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, transformWeigth * leftFootWeight);
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, transformWeigth * rightFootWeight);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, transformWeigth * rightFootWeight);

                    WalkRunIK();
                }
            }
            else
			{	
				if (transformWeigth != 0.0f)
                {
					transformWeigth = Mathf.Lerp(transformWeigth, 0.0f, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);

					if (transformWeigth <= 0.01)
						transformWeigth = 0.0f;
				}

				myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,transformWeigth);
				myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,transformWeigth);
				myAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot,transformWeigth);
				myAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot,transformWeigth);
			}
		}
	}
    #endregion

    #region Intern Behaviour
    private void IdleIK()
    {
		RaycastHit hit;

		leftFootPosition = myAnimator.GetIKPosition(AvatarIKGoal.LeftFoot);

		if (Physics.Raycast(leftFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

			myAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + leftFootOffset);
			myAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(leftFoot.forward, hit.normal),  hit.normal));

            leftFootPosition = hit.point;
		}				

		rightFootPosition = myAnimator.GetIKPosition(AvatarIKGoal.RightFoot);

		if (Physics.Raycast(rightFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);

			myAnimator.SetIKPosition(AvatarIKGoal.RightFoot,hit.point + rightFootOffset);
			myAnimator.SetIKRotation(AvatarIKGoal.RightFoot,Quaternion.LookRotation(Vector3.ProjectOnPlane(rightFoot.forward, hit.normal),  hit.normal));

            rightFootPosition = hit.point;
		}
	}

	private void WalkRunIK()
    {
		RaycastHit hit;

		leftFootPosition = myAnimator.GetIKPosition(AvatarIKGoal.LeftFoot);

		if (Physics.Raycast(leftFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

			myAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + leftFootOffset);
			myAnimator.SetIKRotation(AvatarIKGoal.LeftFoot,Quaternion.LookRotation(Vector3.ProjectOnPlane(leftFoot.forward, hit.normal),  hit.normal));

			leftFootPosition = hit.point;
		}
        			
		rightFootPosition = myAnimator.GetIKPosition(AvatarIKGoal.RightFoot);

		if (Physics.Raycast(rightFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);

			myAnimator.SetIKPosition(AvatarIKGoal.RightFoot,hit.point + rightFootOffset);
			myAnimator.SetIKRotation(AvatarIKGoal.RightFoot,Quaternion.LookRotation(Vector3.ProjectOnPlane(rightFoot.forward, hit.normal),  hit.normal));

			rightFootPosition = hit.point;
		}				
	}
	
	void IdleUpdateCollider () 
	{	
		float dif = leftFootPosition.y - rightFootPosition.y;

		if (dif < 0)
            dif *= -1;

		myCapsuleCollider.center = new Vector3(0, Mathf.Lerp(myCapsuleCollider.center.y, newColliderCenter.y + dif, Time.deltaTime) ,0);
		myCapsuleCollider.height = Mathf.Lerp(myCapsuleCollider.height, newColliderHeight - (dif / 2), Time.deltaTime);
	}

	void WalkRunUpdateCollider () 
	{
		RaycastHit hit;
		Vector3 myGround = Vector3.zero;
		Vector3 dif = Vector3.zero;

		if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 3.0f, raycastLayerMask))
			myGround = hit.point;
		if (Physics.Raycast(transform.position + (((transform.forward * (myCapsuleCollider.radius))) + (myCapsuleCollider.attachedRigidbody.velocity * Time.deltaTime)) + Vector3.up, Vector3.down, out hit, 2.0f, raycastLayerMask))
		{
            //Debug.DrawLine(transform.position + (((transform.forward * (myCapsuleCollider.radius))) + (myCapsuleCollider.attachedRigidbody.velocity * Time.deltaTime)) + Vector3.up, hit.point, Color.red);

            dif = hit.point - myGround;

			if (dif.y < 0)
                dif *= -1;
		}
		if (dif.y < 0.5f)
        {
			myCapsuleCollider.center = new Vector3(0, Mathf.Lerp(myCapsuleCollider.center.y, newColliderCenter.y + dif.y, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed) ,0);
			myCapsuleCollider.height = Mathf.Lerp(myCapsuleCollider.height, newColliderHeight - (dif.y / 2), Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);
		}
        else
        {
			myCapsuleCollider.center = new Vector3(0, Mathf.Lerp(myCapsuleCollider.center.y, newColliderCenter.y, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed) ,0);
			myCapsuleCollider.height = Mathf.Lerp(myCapsuleCollider.height, newColliderHeight, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);
		}
	}
    #endregion
}