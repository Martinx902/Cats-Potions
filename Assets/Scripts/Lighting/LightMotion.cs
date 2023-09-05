using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMotion : MonoBehaviour
{
    [SerializeField] 
    Vector2 cycleDuration = new Vector2(20f, 20f);

    [SerializeField]
    AnimationCurve movementX;

    [SerializeField]
    AnimationCurve movementY;

    [SerializeField]
    Vector2 movementMagnitudeXY = new Vector2(1,1);

    [SerializeField]
    Vector2 movementTimeOffset = new Vector2();

    [SerializeField]
    float magnitudeForce = 20f;

    private Vector3 initPosition;

    private void Awake()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMotion();
    }

    private void UpdateMotion()
    {
        float timeX = Time.time % cycleDuration.x;
        timeX /= cycleDuration.x;


        float timeY = Time.time % cycleDuration.y;
        timeY /= cycleDuration.y;

        float newX = movementX.Evaluate(timeX + movementTimeOffset.x) * movementMagnitudeXY.x * magnitudeForce;
        float nexY = movementY.Evaluate(timeY + movementTimeOffset.y) * movementMagnitudeXY.y * magnitudeForce;

        transform.position = initPosition + new Vector3(newX, 0, nexY);

        initPosition = transform.position;
    }
}
