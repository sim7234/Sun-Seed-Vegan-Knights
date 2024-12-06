using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusLevel : MonoBehaviour
{
    
    public float halfXBounds = 20.0f;
 
    public float halfYBounds = 15.0f;
    [HideInInspector]
    public float halfZBounds = 15.0f;

    [HideInInspector]
    public Bounds focusBounds;

    private void Update()
    {
        Vector3 position = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(position.x - halfXBounds, position.y - halfYBounds, position.z - halfZBounds));
        bounds.Encapsulate(new Vector3(position.x + halfXBounds, position.y + halfYBounds, position.z + halfZBounds));
        focusBounds = bounds;
    }
}
