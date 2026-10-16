using UnityEngine;

[CreateAssetMenu(fileName = "MovementValues", menuName = "Scriptable Objects/MovementValues")]
public class MovementValues : ScriptableObject
{
    public float moveForce = 25;
    public float maxSpeed = 10;
    public float decelerationSpeed = 2.5f;
    public float turnSpeed = 10;
}
