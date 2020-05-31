using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    Vector3 roundPlayerPos;

    [Tooltip("How fast camera shall roll to the next room")]
    [Range(0,1)]
    public float camSmooth = 0.5f;

    void Start() {
        //StartCoroutine(ShakeCam(0.25f,1));
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        roundPlayerPos = new Vector3(Mathf.RoundToInt(player.position.x/16)*16,Mathf.RoundToInt(player.position.y/9)*9,0) + new Vector3(0,0,-10);
        transform.position = Vector3.Lerp(transform.position, roundPlayerPos, camSmooth);
    }

    public IEnumerator ShakeCam(float duration, float magnitude) {
        Vector3 originalPosition = transform.localPosition;
        float originalSize = GetComponent<Camera>().orthographicSize;

        float elapsed = 0;

        while(elapsed < duration) {
            Vector2 newPosition = Random.insideUnitCircle * magnitude;
            float newSize = -Random.value * magnitude;
            transform.localPosition = (Vector3) newPosition + originalPosition;
            GetComponent<Camera>().orthographicSize = newSize + originalSize;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
