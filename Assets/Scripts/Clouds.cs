using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPreviousPosition;
    void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }

        targetPreviousPosition = followingTarget.position;

    }

    
    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;
        if (disableVerticalParallax)
        {
            delta.y = 0f;
        }
        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxStrength;
    }
}
