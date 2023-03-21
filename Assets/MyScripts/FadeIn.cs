using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    public float fadeTime = 1.0f;
    public KeyCode activateKey = KeyCode.Space;

    private Camera cam;
    private Image image;
    private float startTime;
    private bool activated = false;

    void Start() {
        cam = Camera.main;
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    void Update() {
        if (activated) {
            if (cam.depthTextureMode == DepthTextureMode.None) {
                cam.depthTextureMode = DepthTextureMode.Depth;
            }

            if (cam.depthTextureMode == DepthTextureMode.Depth && cam.depthTextureMode == DepthTextureMode.DepthNormals) {
                Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(transform.position));
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo)) {
                    if (hitInfo.distance < cam.transform.position.z) {
                        float elapsedTime = Time.time - startTime;
                        float alpha = Mathf.Clamp01(elapsedTime / fadeTime);
                        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(activateKey)) {
            activated = true;
            startTime = Time.time;
        }
    }
}
