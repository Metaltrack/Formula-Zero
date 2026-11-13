using UnityEngine;

public class InterpolatedCamera : MonoBehaviour
{
    [Header("Camera Properties")]
    public Transform targetObject;
    public Vector3 transformOffset;
    public float lerpSpeed = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!targetObject)
        {
            return;
        }

        Vector3 transformXForm = targetObject.TransformPoint(transformOffset);
        Vector3 pos;
        pos.x = Mathf.Lerp(transform.position.x, transformXForm.x, lerpSpeed * Time.deltaTime);
        pos.y = Mathf.Lerp(transform.position.y, transformXForm.y, lerpSpeed * Time.deltaTime);
        pos.z = Mathf.Lerp(transform.position.z, transformXForm.z, lerpSpeed * Time.deltaTime);
        transform.position = pos;

        transform.LookAt(targetObject.position, targetObject.up);
    }
}
