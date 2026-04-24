using System;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    [SerializeField] public Boolean rotation;
    [SerializeField] public Boolean xAxis;
    [SerializeField] public Boolean yAxis;
    [SerializeField] public Boolean zAxis;
    [SerializeField] public int rotationSpeed;

    [SerializeField] public Boolean orbit;
    float orbitRadius;
    [SerializeField] public float orbitSpeed;
    [SerializeField] public Transform orbitPoint;
    [SerializeField] public Boolean xAxisOrbit;
    [SerializeField] public Boolean yAxisOrbit;
    [SerializeField] public Boolean zAxisOrbit;

    private void FixedUpdate()
    {
        //RotationTransformation();
        OrbitTransformation();
    }

    private void RotationTransformation()
    {
        if (!rotation)
        {
            return;
        }
        float xChange = xAxis ? Time.deltaTime * rotationSpeed : 0;
        float yChange = yAxis ? Time.deltaTime * rotationSpeed : 0;
        float zChange = zAxis ? Time.deltaTime * rotationSpeed : 0;
        transform.Rotate(xChange, yChange, zChange);
    }

    private void OrbitTransformation()
    {
        if (!orbit)
        {
            return;
        }
        orbitRadius = Vector3.Distance(this.transform.position,orbitPoint.transform.position);
        float angle = Time.deltaTime * orbitSpeed;
        Quaternion rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.up);
        Vector3 orbitRotation = new(xAxisOrbit ? orbitRadius : 0, yAxisOrbit ? orbitRadius : 0, zAxisOrbit ? orbitRadius : 0);

        this.transform.position = orbitPoint.position + rotation * orbitRotation;
    }
}
