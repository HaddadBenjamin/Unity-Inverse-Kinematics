using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class TransformExtension
{
    public static void DestroyAllChilds(this Transform transform)
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
    }

    public static Transform GetNearestTransform(this Transform transform, Transform[] lookingTransforms)
    {
        if (null == transform)
            return null;

        Transform nearestTransform = null;
        float maxDistance = float.MaxValue;

        for (int gameObjectIndex = 0; gameObjectIndex < lookingTransforms.Length; gameObjectIndex++)
        {
            if (null == lookingTransforms[gameObjectIndex])
                return null;

            float distanceFromGameObject = Vector3.Distance(lookingTransforms[gameObjectIndex].position, transform.position);

            if (distanceFromGameObject < maxDistance)
            {
                maxDistance = distanceFromGameObject;
                nearestTransform = lookingTransforms[gameObjectIndex];
            }
        }

        return null == nearestTransform ? null : nearestTransform.transform;
    }

    public static Transform GetNearestTransformInDistance(this Transform transform, Transform[] lookingTransforms, float distance)
    {
        List<Transform> tranformsInDistance = new List<Transform>();

        foreach (Transform transformInDistance in tranformsInDistance)
        {
            if (Vector3.Distance(transformInDistance.position, transform.position) < distance)
                tranformsInDistance.Add(transformInDistance);
        }

        return transform.GetNearestTransform(tranformsInDistance.ToArray());
    }

    public static Transform GetNearestTransformWithTagAndDistance(this Transform transform, string tag, float distance)
    {
        return transform.GetNearestTransformInDistance(
            Array.ConvertAll(GameObject.FindGameObjectsWithTag(tag), gameObject => gameObject.transform),
            distance);
    }


    public static Transform GetNearestTransformWithTag(this Transform transform, string tag)
    {
        return transform.GetNearestTransform(
            Array.ConvertAll(GameObject.FindGameObjectsWithTag(tag), gameobject => gameobject.transform));
    }


    public static Transform[] GetAllTransformInDistance(this Transform transform, Transform[] lookingTransforms, float distance)
    {
        List<Transform> tranformsInDistance = new List<Transform>();

        float maxDistance = float.MaxValue;

        for (int gameObjectIndex = 0; gameObjectIndex < lookingTransforms.Length; gameObjectIndex++)
        {
            float distanceFromGameObject = Vector3.Distance(lookingTransforms[gameObjectIndex].position, transform.position);

            if (distanceFromGameObject < maxDistance)
            {
                maxDistance = distanceFromGameObject;
                tranformsInDistance.Add(lookingTransforms[gameObjectIndex]);
            }
        }

        return 0 == tranformsInDistance.Count ?
            null :
            tranformsInDistance.ToArray();
    }

    public static Transform[] GetAllTransformWithTagAndDistance(this Transform transform, string tag, float distance)
    {
        return transform.GetAllTransformInDistance(
            Array.ConvertAll(GameObject.FindGameObjectsWithTag(tag),
                                gameobject => gameobject.transform),
                                distance);
    }

    public static void ResetTransform(this Transform transform)
    {
        transform.position = Camera.main.transform.position;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public static void SetPositionAndRotationAndParent(this Transform transform, Vector3 position, Vector3 rotation, Transform parent)
    {
        transform.SetParent(parent);

        transform.localPosition = position;
        transform.localEulerAngles = rotation;
    }

    public static void SetPositionAndParent(this Transform transform, Vector3 position, Transform parent)
    {
        transform.SetParent(parent);

        transform.localPosition = position;
    }

    public static void SetPositionX(this Transform transform, float positionX)
    {
        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
    }

    public static void SetPositionY(this Transform transform, float positionY)
    {
        transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
    }

    public static void SetPositionZ(this Transform transform, float positionZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, positionZ);
    }

    public static void SetLocalPositionX(this Transform transform, float positionX)
    {
        transform.localPosition = new Vector3(positionX, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform transform, float positionY)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, positionY, transform.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform transform, float positionZ)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, positionZ);
    }

    public static void SetRotationX(this Transform transform, float rotationX)
    {
        transform.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public static void SetRotationY(this Transform transform, float rotationY)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
    }

    public static void SetRotationZ(this Transform transform, float rotationZ)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);
    }

    public static void SetLocalRotationX(this Transform transform, float rotationX)
    {
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public static void SetLocalRotationY(this Transform transform, float rotationY)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, transform.localEulerAngles.z);
    }

    public static void SetLocalRotationZ(this Transform transform, float rotationZ)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotationZ);
    }

    public static void SetScaleX(this Transform transform, float scaleX)
    {
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    public static void SetScaleY(this Transform transform, float scaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    public static void SetScaleZ(this Transform transform, float scaleZ)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scaleZ);
    }

    public static void FollowCursorPosition(this Transform transform, float distanceFromMainCamera)
    {
        Vector3 position = Input.mousePosition;
        position.z = distanceFromMainCamera;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }

    public static void FollowCursorPositionWithDefinedHeight(this Transform transform, float definedHeight)
    {
        Vector3 position = Input.mousePosition;
        // position.y = definedHeight;
        transform.position = Camera.main.ScreenToWorldPoint(position);
        //Vector3 transformPosition = transform.position;
        //transform.position = new Vector3(transformPosition.x, definedHeight, transformPosition.z);
    }

    public static void FolowMousePosition(this Transform transform, float speed = float.MaxValue)
    {
        Plane planelane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        if (planelane.Raycast(ray, out hitDistance))
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    public static Transform[] GetChildrens(this Transform transform)
    {
        return transform.GetComponentsInChildren<Transform>();
    }
}