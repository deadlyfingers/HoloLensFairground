using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BottleScript : MonoBehaviour, IInputHandler
{
    private Vector3 _position;
    private Quaternion _rotation;
    private Color _color;
    private Rigidbody _rigidbody;
    private AudioSource _audio;

    public Color OriginalColor {
        get {
            return _color;
        }
    }

    void Awake()
    {
        // Save orginal properties
        _position = gameObject.transform.localPosition;
        _rotation = gameObject.transform.localRotation;
        _color = GetComponent<Renderer>().material.color;
        _rigidbody = GetComponent<Rigidbody>();

        // (4) Spatial Sound: Retrieve Audio Source on the bottle
        _audio = GetComponent<AudioSource>();

        ResetBottle();
    }

    // (2) Gesture Input: Implement the IInputHandler interface to handle tap events on the bottle.
    public void OnInputDown(InputEventData eventData)
    {
        Tapped();
    }
    public void OnInputUp(InputEventData eventData)
    {
    }

    public void Tapped()
    {
        // (2) Gesture Input: When the bottle is selected apply a force in opposite direction to 
        // the normal at the hit location
        var rayHit = GazeManager.Instance.HitInfo;
        _rigidbody.AddForceAtPosition(rayHit.normal * -5, rayHit.point, ForceMode.Impulse);

        // (4) Spatial Sound: Play audio when we tap
        _audio.pitch = Random.Range(0.5f, 2.0f);
        _audio.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        // (4) Spatial Sound: Play audio when we have a collision
        if (collision.relativeVelocity.magnitude > 2)
        {
            _audio.pitch = Random.Range(0.5f, 2.0f);
            _audio.Play();
        }
    }

    public void ResetBottle()
    {
        // Disable physics until we have placed the stage (otherwise the bottles will just fall over!)
        SuspendPhysics();
        gameObject.transform.localPosition = _position;
        gameObject.transform.localRotation = _rotation;
        RestorePhysics();
    }

    private void SuspendPhysics()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.detectCollisions = false;
        _rigidbody.useGravity = false;
    }

    private void RestorePhysics()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.detectCollisions = true;
        _rigidbody.useGravity = true;
        _rigidbody.Sleep();
    }

}
