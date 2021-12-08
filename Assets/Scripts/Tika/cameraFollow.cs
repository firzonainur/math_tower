// using SuperTiled2Unity;
// using UnityEngine;

// public class cameraFollow : MonoBehaviour
// {
//     private Vector2 minValues, maxValues;
//     private float m_CurrentAspect;
//     public Transform target;
//     public GameObject m_Tilemap;
//     public Grid m_TilemapGrid;

//     [Range(1, 10)]
//     public float m_SmoothFactor;

//     private void CalculateCameraBounds()
//     {
//         // Map Width and Height
//         SuperMap superScript = m_Tilemap.GetComponent<SuperMap>();
//         float mapWidth = m_TilemapGrid.cellSize.x * superScript.m_Width;
//         float mapHeight = m_TilemapGrid.cellSize.y * superScript.m_Height;

//         // Camera Width and Height
//         Camera cam = Camera.main;
//         float height = 2f * cam.orthographicSize;
//         float halfWidth = (height * cam.aspect) / 2;
//         float halfHeight = cam.orthographicSize;

//         // Calculate Camera Bounds
//         minValues = new Vector2(halfWidth, -mapHeight + halfHeight);
//         maxValues = new Vector2(mapWidth - halfWidth, -halfHeight);
//     }

//     private void ClampCameraPosition(bool smoothed)
//     {
//         Vector2 boundPosition = new Vector2(
//             Mathf.Clamp(target.position.x, minValues.x, maxValues.x),
//             Mathf.Clamp(target.position.y, minValues.y, maxValues.y)
//         );

//         if (smoothed)
//         {
//             Vector2 smoothPosition = Vector2.Lerp(transform.position, boundPosition, m_SmoothFactor * Time.deltaTime);
//             transform.position = new Vector3(smoothPosition.x, smoothPosition.y, -10);
//         }
//         else
//         {
//             transform.position = boundPosition;
//         }
//     }

//     void Start()
//     {
//         m_CurrentAspect = Camera.main.aspect;
//         CalculateCameraBounds();
//         ClampCameraPosition(false);
//     }

//     void Update()
//     {
//         if (m_CurrentAspect != Camera.main.aspect)
//         {
//             m_CurrentAspect = Camera.main.aspect;
//             CalculateCameraBounds();
//             ClampCameraPosition(false);
//         }
//     }

//     private void FixedUpdate()
//     {
//         ClampCameraPosition(true);
//     }
// }

using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    [Range(1, 10)]
    public float m_SmoothFactor;

    public Vector2 minValues;
    public Vector2 maxValues;

    public Vector3 cameraOffset;

    public float zoom;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(target.position.x, minValues.x, maxValues.x),
            Mathf.Clamp(target.position.y, minValues.y, maxValues.y)
        );

        Vector2 smoothPosition = Vector2.Lerp(transform.position, boundPosition, m_SmoothFactor * Time.deltaTime);
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, zoom);
    }
}