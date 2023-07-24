using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Material spottedMaterial;
    Mesh mesh;
    Vector3 origin = Vector3.zero;
    float viewDistance = 5;
    float startingAngle = 0f;
    float fov = 90f;

    private void Start()
    {
        mesh = new Mesh();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Enemy FOV";
    }

    private void LateUpdate()
    {
        int rayCount = 15; //not including 'zeroth' array   
        float angle = startingAngle;
        float angleIncrement = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; // 1 for origin, 1 for zeroth array
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3]; // one triangle in between 2 rays 

        vertices[0] = origin;

        int vertexIndex = 1; // starts from 1, since zero is origin
        int trianglesIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            Physics2D.queriesHitTriggers = false;
            RaycastHit2D hitInfo = Physics2D.Raycast(origin, myUtilities.Angle2Vector(angle), viewDistance, layerMask);

            if (hitInfo.collider == null)
            {
                vertex = origin + (myUtilities.Angle2Vector(angle) * viewDistance);
            }
            else
            {
                vertex = hitInfo.point;
            }

            vertices[vertexIndex] = vertex;

            if (vertexIndex > 1) // only needed from the third vertex onwards (equiv. to if(i>0))
            {
                triangles[trianglesIndex] = 0;
                triangles[trianglesIndex + 1] = vertexIndex - 1;
                triangles[trianglesIndex + 2] = vertexIndex;

                trianglesIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrement; //unity angle works ACW, thus minus to get clockwise
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000);
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = myUtilities.Vector2Angle(aimDirection) + fov / 2f; // moves the starting angle
    }

    public void SetFOV(float fov)
    {
        this.fov = fov;
    }

    public void Spotted()
    {
        GetComponent<MeshRenderer>().material = spottedMaterial;
    }
}
