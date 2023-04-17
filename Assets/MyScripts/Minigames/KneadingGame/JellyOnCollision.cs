using UnityEngine;

public class JellyOnCollision : MonoBehaviour
{
    public float intensity = 1f;
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;

    private Mesh originalMesh;
    private Mesh meshClone;
    private MeshRenderer meshRenderer;
    private JellyVertex[] jellyVertices;
    private Vector3[] vertexArray;

    private bool isJellyActive = false;
    private int jellyCounter = 0;


    private void Start()
    {
        Debug.Log("starts the jelly on collision script");
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        meshRenderer = GetComponent<MeshRenderer>();
        jellyVertices = new JellyVertex[meshClone.vertices.Length];

        for (int i = 0; i < meshClone.vertices.Length; i++)
        {
            jellyVertices[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
        }

        GetComponent<MeshFilter>().sharedMesh = meshClone;
    }

    private void FixedUpdate()
    {
        if ((isJellyActive && jellyCounter == 10))
        {
            jellyCounter = 0;
            vertexArray = originalMesh.vertices;
            Debug.Log("jelly activities");

            for (int i = 0; i < jellyVertices.Length; i++)
            {
                Debug.Log("bouncin");
                Vector3 target = transform.TransformPoint(vertexArray[jellyVertices[i].ID]);
                float jellyIntensity = (1 - (meshRenderer.bounds.max.y - target.y) / meshRenderer.bounds.size.y) * intensity;
                jellyVertices[i].Shake(target, mass, stiffness, damping);
                target = transform.InverseTransformPoint(jellyVertices[i].Position);
                vertexArray[jellyVertices[i].ID] = Vector3.Lerp(vertexArray[jellyVertices[i].ID], target, jellyIntensity);
            }

            meshClone.vertices = vertexArray;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isJellyActive = true;
        jellyCounter += 1;
        Debug.Log("jellyCounter: " + jellyCounter);

        //if (other.gameObject.tag == "Jellyable")
        //{
        //    Debug.Log("jellyable");
        //    isJellyActive = true;
        //}
    }

    private void OnCollisionExit(Collision other)
    {
        isJellyActive = false;

        //if (other.gameObject.tag == "Jellyable")
        //{
        //    isJellyActive = false;
        //}
    }

    public class JellyVertex
    {
        public int ID;
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Force;

        public JellyVertex(int id, Vector3 position)
        {
            ID = id;
            Position = position;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            Force = (target - Position) * s;
            Velocity = (Velocity + Force / m) * d;
            Position += Velocity;

            if ((Velocity + Force + Force / m).magnitude < 0.001f)
            {
                Position = target;
            }
        }
    }
}
