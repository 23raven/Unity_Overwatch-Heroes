using UnityEngine;

public class BackgroundBlockGenerator : MonoBehaviour
{
    public Transform plane;

    [Header("Grid")]
    public float cellSize = 6f;
    public float edgeThickness = 15f;

    [Header("Heights")]
    public Vector2 heightRange = new Vector2(5f, 40f);

    public Material blockMaterial;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        Vector3 planeSize = plane.localScale * 10f;
        float halfW = planeSize.x / 2f;
        float halfD = planeSize.z / 2f;

        GenerateEdgeRow(
            start: new Vector3(-halfW, 0, halfD - edgeThickness),
            direction: Vector3.right,
            length: planeSize.x,
            inward: Vector3.forward
        );

        GenerateEdgeRow(
            start: new Vector3(-halfW, 0, -halfD),
            direction: Vector3.right,
            length: planeSize.x,
            inward: Vector3.back
        );

        GenerateEdgeRow(
            start: new Vector3(halfW - edgeThickness, 0, -halfD),
            direction: Vector3.forward,
            length: planeSize.z,
            inward: Vector3.right
        );

        GenerateEdgeRow(
            start: new Vector3(-halfW, 0, -halfD),
            direction: Vector3.forward,
            length: planeSize.z,
            inward: Vector3.left
        );
    }

    void GenerateEdgeRow(
     Vector3 start,
     Vector3 direction,
     float length,
     Vector3 inward
 )
    {
        int cells = Mathf.FloorToInt(length / cellSize);
        int depthLayers = Mathf.FloorToInt(edgeThickness / cellSize);

        for (int layer = 0; layer < depthLayers; layer++)
        {
            float layerDepth = layer * cellSize;

            for (int i = 0; i < cells; i++)
            {
                float baseHeight = Random.Range(heightRange.x, heightRange.y);
                float heightFalloff = Mathf.Lerp(1f, 0.5f, (float)layer / depthLayers);
                float height = baseHeight * heightFalloff;

                Vector3 pos =
                    plane.position +
                    start +
                    direction * (i * cellSize + cellSize / 2f) +
                    inward * (layerDepth + cellSize / 2f);

                GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.transform.position = pos + Vector3.up * (height / 2f);

                float scaleXZ = Random.Range(0.8f, 1.1f);
                block.transform.localScale = new Vector3(
                    cellSize * scaleXZ,
                    height,
                    cellSize * scaleXZ
                );

                block.transform.SetParent(transform);

                if (blockMaterial)
                    block.GetComponent<Renderer>().material = blockMaterial;
            }
        }
    }

}
