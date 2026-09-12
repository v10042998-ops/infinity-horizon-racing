using UnityEngine;
using System.Collections.Generic;

public class ProceduralWorldGenerator : MonoBehaviour
{
    [Header("World Settings")]
    [SerializeField] private int worldSeed = 12345;
    [SerializeField] private float chunkSize = 100f;
    [SerializeField] private float roadWidth = 8f;
    [SerializeField] private Material roadMaterial;
    [SerializeField] private Material terrainMaterial;
    
    [Header("Procedural Settings")]
    [SerializeField] private float perlinScale = 0.1f;
    [SerializeField] private float heightMultiplier = 30f;
    [SerializeField] private int meshResolution = 50;
    
    private Dictionary<Vector2, GameObject> generatedChunks = new Dictionary<Vector2, GameObject>();
    private Transform player;
    private Vector2 currentChunkCoord = Vector2.zero;
    private float chunkLoadDistance = 300f;
    
    private void Start()
    {
        Random.InitState(worldSeed);
        player = FindObjectOfType<VehicleController>()?.transform;
        GenerateInitialChunks();
    }
    
    private void Update()
    {
        if (player != null)
        {
            Vector2 playerChunkCoord = GetChunkCoordinate(player.position);
            if (playerChunkCoord != currentChunkCoord)
            {
                currentChunkCoord = playerChunkCoord;
                UpdateChunks();
            }
        }
    }
    
    private void GenerateInitialChunks()
    {
        Vector2 center = Vector2.zero;
        for (int x = -2; x <= 2; x++)
        {
            for (int z = -2; z <= 2; z++)
            {
                Vector2 chunkCoord = new Vector2(x, z);
                GenerateChunk(chunkCoord);
            }
        }
    }
    
    private void UpdateChunks()
    {
        // Generate new chunks
        for (int x = -2; x <= 2; x++)
        {
            for (int z = -2; z <= 2; z++)
            {
                Vector2 chunkCoord = currentChunkCoord + new Vector2(x, z);
                if (!generatedChunks.ContainsKey(chunkCoord))
                {
                    GenerateChunk(chunkCoord);
                }
            }
        }
        
        // Unload distant chunks
        List<Vector2> chunksToRemove = new List<Vector2>();
        foreach (var chunk in generatedChunks)
        {
            float distance = Vector2.Distance(chunk.Key, currentChunkCoord);
            if (distance > chunkLoadDistance / chunkSize)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }
        
        foreach (var chunk in chunksToRemove)
        {
            Destroy(generatedChunks[chunk]);
            generatedChunks.Remove(chunk);
        }
    }
    
    private void GenerateChunk(Vector2 chunkCoord)
    {
        GameObject chunkObject = new GameObject($"Chunk_{chunkCoord.x}_{chunkCoord.y}");
        chunkObject.transform.parent = transform;
        chunkObject.transform.position = new Vector3(chunkCoord.x * chunkSize, 0, chunkCoord.y * chunkSize);
        
        // Generate terrain mesh
        Mesh terrainMesh = GenerateTerrainMesh(chunkCoord);
        
        MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshFilter.mesh = terrainMesh;
        
        MeshRenderer meshRenderer = chunkObject.AddComponent<MeshRenderer>();
        meshRenderer.material = terrainMaterial;
        
        MeshCollider meshCollider = chunkObject.AddComponent<MeshCollider>();
        meshCollider.convex = false;
        meshCollider.mesh = terrainMesh;
        
        // Generate roads
        GenerateRoadsInChunk(chunkObject, chunkCoord);
        
        generatedChunks[chunkCoord] = chunkObject;
    }
    
    private Mesh GenerateTerrainMesh(Vector2 chunkCoord)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[meshResolution * meshResolution];
        
        for (int z = 0; z < meshResolution; z++)
        {
            for (int x = 0; x < meshResolution; x++)
            {
                int index = z * meshResolution + x;
                float xPos = (x / (float)(meshResolution - 1)) * chunkSize;
                float zPos = (z / (float)(meshResolution - 1)) * chunkSize;
                
                // World position for Perlin noise
                float worldX = chunkCoord.x * chunkSize + xPos;
                float worldZ = chunkCoord.y * chunkSize + zPos;
                
                float height = Mathf.PerlinNoise(worldX * perlinScale, worldZ * perlinScale) * heightMultiplier;
                vertices[index] = new Vector3(xPos, height, zPos);
            }
        }
        
        // Create triangles
        int[] triangles = new int[(meshResolution - 1) * (meshResolution - 1) * 6];
        int triIndex = 0;
        
        for (int z = 0; z < meshResolution - 1; z++)
        {
            for (int x = 0; x < meshResolution - 1; x++)
            {
                int a = z * meshResolution + x;
                int b = z * meshResolution + (x + 1);
                int c = (z + 1) * meshResolution + x;
                int d = (z + 1) * meshResolution + (x + 1);
                
                triangles[triIndex++] = a;
                triangles[triIndex++] = c;
                triangles[triIndex++] = b;
                triangles[triIndex++] = b;
                triangles[triIndex++] = c;
                triangles[triIndex++] = d;
            }
        }
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        return mesh;
    }
    
    private void GenerateRoadsInChunk(GameObject chunkObject, Vector2 chunkCoord)
    {
        // Simple road generation along cardinal directions
        if ((int)chunkCoord.x % 2 == 0)
        {
            CreateRoad(chunkObject, Vector3.forward, "NS_Road");
        }
        if ((int)chunkCoord.y % 2 == 0)
        {
            CreateRoad(chunkObject, Vector3.right, "EW_Road");
        }
    }
    
    private void CreateRoad(GameObject parent, Vector3 direction, string name)
    {
        GameObject road = new GameObject(name);
        road.transform.parent = parent.transform;
        road.transform.localPosition = Vector3.zero;
        
        Mesh roadMesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        
        if (direction == Vector3.forward)
        {
            vertices[0] = new Vector3(-roadWidth / 2, 0.1f, 0);
            vertices[1] = new Vector3(roadWidth / 2, 0.1f, 0);
            vertices[2] = new Vector3(-roadWidth / 2, 0.1f, chunkSize);
            vertices[3] = new Vector3(roadWidth / 2, 0.1f, chunkSize);
        }
        else
        {
            vertices[0] = new Vector3(0, 0.1f, -roadWidth / 2);
            vertices[1] = new Vector3(0, 0.1f, roadWidth / 2);
            vertices[2] = new Vector3(chunkSize, 0.1f, -roadWidth / 2);
            vertices[3] = new Vector3(chunkSize, 0.1f, roadWidth / 2);
        }
        
        int[] triangles = { 0, 2, 1, 1, 2, 3 };
        
        roadMesh.vertices = vertices;
        roadMesh.triangles = triangles;
        roadMesh.RecalculateNormals();
        
        MeshFilter meshFilter = road.AddComponent<MeshFilter>();
        meshFilter.mesh = roadMesh;
        
        MeshRenderer meshRenderer = road.AddComponent<MeshRenderer>();
        meshRenderer.material = roadMaterial;
        
        MeshCollider meshCollider = road.AddComponent<MeshCollider>();
        meshCollider.mesh = roadMesh;
    }
    
    private Vector2 GetChunkCoordinate(Vector3 position)
    {
        return new Vector2(
            Mathf.Floor(position.x / chunkSize),
            Mathf.Floor(position.z / chunkSize)
        );
    }
    
    public int GetWorldSeed() => worldSeed;
}
