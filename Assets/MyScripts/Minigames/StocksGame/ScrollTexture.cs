using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    private Material mat;
    private Vector3 lastPosition;
    private Vector2 texScale;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        lastPosition = transform.position;
        texScale = mat.GetTextureScale("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        // Get current offset of texture and change it
        Vector2 oldOffset = mat.GetTextureOffset("_MainTex");
        float dZ = lastPosition.z - transform.position.z;
        float dX = lastPosition.x - transform.position.x;
        Vector2 newOffset = new Vector2(oldOffset.x - dX / (transform.localScale.x / texScale.x), 
            oldOffset.y - dZ / (transform.localScale.y / texScale.y));
        mat.SetTextureOffset("_MainTex", newOffset);
        lastPosition = transform.position;
    }
}
