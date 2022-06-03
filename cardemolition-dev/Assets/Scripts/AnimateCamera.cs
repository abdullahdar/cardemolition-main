using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 35f;
    public Transform targetObject;
    private bool movingTowardsTarget = false;

    // Angular speed in radians per sec.
    private bool rotatingTowardsTarget = false;

    public bool focusCamera;    

    private float initialDistance;
    private float currentDistance;

    Quaternion initialRotation;
    void Start()
    {
        //targetObject = LevelManagerGamePlay.instance.GetFocusPoint();        

        initialRotation = transform.rotation;
        initialDistance = Vector3.Distance(transform.position, targetObject.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f") || focusCamera)
        {
            if (movingTowardsTarget == true)
            {
                movingTowardsTarget = false;
                rotatingTowardsTarget = false;
            }
            else
            {                
                movingTowardsTarget = true;
                rotatingTowardsTarget = true;
            }
            focusCamera = false;
        }

        if (movingTowardsTarget)
            MoveTowardsTarget(targetObject);


        if (rotatingTowardsTarget)
            RotateTowardsTarget(targetObject);
    }

    void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.transform.position) < 0.1)
        {
            transform.position = target.transform.position;
            movingTowardsTarget = false;
            rotatingTowardsTarget = false;                        
        }
    }

    void RotateTowardsTarget(Transform target)
    {
        currentDistance = Vector3.Distance(target.position, transform.position);

        float t = currentDistance / initialDistance;

        transform.rotation = Quaternion.Lerp(target.rotation, initialRotation, t);
    }


}
