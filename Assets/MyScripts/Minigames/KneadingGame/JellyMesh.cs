using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMesh : MonoBehaviour
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

    private void Start()
    {
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
        if (isJellyActive)
        {
            vertexArray = originalMesh.vertices;

            for (int i = 0; i < jellyVertices.Length; i++)
            {
                Vector3 target = transform.TransformPoint(vertexArray[jellyVertices[i].ID]);
                float jellyIntensity = (1 - (meshRenderer.bounds.max.y - target.y) / meshRenderer.bounds.size.y) * intensity;
                jellyVertices[i].Shake(target, mass, stiffness, damping);
                target = transform.InverseTransformPoint(jellyVertices[i].Position);
                vertexArray[jellyVertices[i].ID] = Vector3.Lerp(vertexArray[jellyVertices[i].ID], target, jellyIntensity);
            }

            meshClone.vertices = vertexArray;
        }
    }

    private void OnMouseDown()
    {
        isJellyActive = !isJellyActive;
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








//using System.Collections;

//using System.Collections.Generic;

//using UnityEngine;



//public class JellyMesh : MonoBehaviour

//{

//    public float Intensity = 1f;

//    public float Mass = 1f;

//    public float stiffness = 1f;

//    public float damping = 0.75f;



//    private Mesh OriginalMesh, MeshClone;

//    private MeshRenderer renderer;

//    private JellyVertex[] jv;

//    private Vector3[] vertexArray;

//    // Start is called before the first frame update

//    void Start()

//    {


//        OriginalMesh = GetComponent<MeshFilter>().sharedMesh;

//        MeshClone = Instantiate(OriginalMesh);


//        Debug.Log("MeshClone: " + MeshClone.isReadable);

//        GetComponent<MeshFilter>().sharedMesh = MeshClone;

//        renderer = GetComponent<MeshRenderer>();

//        jv = new JellyVertex[MeshClone.vertices.Length];

//        for (int i = 0; i < MeshClone.vertices.Length; i++)

//        {

//            jv[i] = new JellyVertex(i, transform.TransformPoint(MeshClone.vertices[i]));

//        }



//    }



//    // Update is called once per frame

//    void FixedUpdate()

//    {

//        vertexArray = OriginalMesh.vertices;

//        for (int i = 0; i < jv.Length; i++)

//        {

//            Vector3 target = transform.TransformPoint(vertexArray[jv[i].ID]);

//            float intensity = (1 - (renderer.bounds.max.y - target.y) / renderer.bounds.size.y) * Intensity;

//            jv[i].Shake(target, Mass, stiffness, damping);

//            target = transform.InverseTransformPoint(jv[i].Position);

//            vertexArray[jv[i].ID] = Vector3.Lerp(vertexArray[jv[i].ID], target, intensity);



//        }

//        MeshClone.vertices = vertexArray;

//    }



//    public class JellyVertex

//    {

//        public int ID;

//        public Vector3 Position;

//        public Vector3 velocity, Force;



//        public JellyVertex(int _id, Vector3 _pos)

//        {

//            ID = _id;

//            Position = _pos;

//        }



//        public void Shake(Vector3 target, float m, float s, float d)

//        {

//            Force = (target - Position) * s;

//            velocity = (velocity + Force / m) * d;

//            Position += velocity;

//            if ((velocity + Force + Force / m).magnitude < 0.001f)

//            {

//                Position = target;

//            }

//        }



//    }

//}