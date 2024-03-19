using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrthographicCamera : MonoBehaviour
{
    Camera m_camera;

    void Awake()
    {
        m_camera = GetComponent<Camera>();

        if (!m_camera.CompareTag("MainCamera"))
            Debug.LogWarning("Camera is not set as Main Camera!");
    }

    void Start()
    {
        var (center, size) = CalculateOrthoSize();

        m_camera.transform.position = center;
        m_camera.orthographicSize = size;
    }

    (Vector3 center, float size) CalculateOrthoSize()
    {
        var bounds = new Bounds();

        foreach (var col in FindObjectsOfType<Collider2D>())
        {
            if (col.CompareTag("Top Goal") || col.CompareTag("Bottom Goal")) continue;
            bounds.Encapsulate(col.bounds);
        }

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * m_camera.pixelHeight / m_camera.pixelWidth;

        var size = Mathf.Max(horizontal, vertical) * 0.5f;
        var center = bounds.center + new Vector3(0, 0, -10);

        return (center, size);
    }
}
