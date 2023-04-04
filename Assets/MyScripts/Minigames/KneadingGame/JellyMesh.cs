using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMesh : MonoBehaviour
{
    public float intensity = 1f;
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;
    public float liftHeight = 1f;
    public float dropSpeed = 2f;

    private Mesh originalMesh;
    private Mesh meshClone;
    private MeshRenderer meshRenderer;
    private JellyVertex[] jellyVertices;
    private Vector3[] vertexArray;

    private bool isJellyActive = false;
    private bool isLifted = false;
    private Vector3 originalPosition;

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

        originalPosition = transform.position;
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
        else if (isLifted)
        {
            transform.position += Vector3.up * Time.deltaTime * dropSpeed;
            if (transform.position.y >= originalPosition.y + liftHeight)
            {
                isLifted = false;
            }
        }
        else if (transform.position.y > originalPosition.y)
        {
            transform.position += Vector3.down * Time.deltaTime * dropSpeed;
        }
    }

    private void OnMouseDown()
    {
        isJellyActive = !isJellyActive;

        if (!isLifted)
        {
            transform.position = new Vector3(transform.position.x, originalPosition.y + liftHeight, transform.position.z);
            isLifted = true;
        }
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



////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class JellyMesh : MonoBehaviour
////{
////    public float intensity = 1f;
////    public float mass = 1f;
////    public float stiffness = 1f;
////    public float damping = 0.75f;

////    private Mesh originalMesh;
////    private Mesh meshClone;
////    private MeshRenderer meshRenderer;
////    private JellyVertex[] jellyVertices;
////    private Vector3[] vertexArray;

////    private bool isJellyActive = false;

////    private void Start()
////    {
////        originalMesh = GetComponent<MeshFilter>().sharedMesh;
////        meshClone = Instantiate(originalMesh);
////        meshRenderer = GetComponent<MeshRenderer>();
////        jellyVertices = new JellyVertex[meshClone.vertices.Length];

////        for (int i = 0; i < meshClone.vertices.Length; i++)
////        {
////            Debug.Log("jelly bounce?");
////            jellyVertices[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
////        }

////        GetComponent<MeshFilter>().sharedMesh = meshClone;
////    }

////    private void FixedUpdate()
////    {
////        if (isJellyActive)
////        {

////            //set the position of the bread to be slightly above the ground
////            vertexArray = originalMesh.vertices;

////            for (int i = 0; i < jellyVertices.Length; i++)
////            {
////                //Debug.Log("bounce");
////                Vector3 target = transform.TransformPoint(vertexArray[jellyVertices[i].ID]);
////                float jellyIntensity = (1 - (meshRenderer.bounds.max.y - target.y) / meshRenderer.bounds.size.y) * intensity;
////                jellyVertices[i].Shake(target, mass, stiffness, damping);
////                target = transform.InverseTransformPoint(jellyVertices[i].Position);
////                vertexArray[jellyVertices[i].ID] = Vector3.Lerp(vertexArray[jellyVertices[i].ID], target, jellyIntensity);
////            }

////            meshClone.vertices = vertexArray;
////        }
////    }

////    private void OnMouseDown()
////    {
////        Debug.Log("mouse clicked down");
////        isJellyActive = !isJellyActive;

////        //if (!isJellyActive)
////        //{
////        //}
////    }

////    public class JellyVertex
////    {
////        public int ID;
////        public Vector3 Position;
////        public Vector3 Velocity;
////        public Vector3 Force;


////        public JellyVertex(int id, Vector3 position)
////        {
////            ID = id;
////            Position = position;
////        }

////        public void Shake(Vector3 target, float m, float s, float d)
////        {
////            //Debug.Log("Velocity: " + Velocity);
////            //Debug.Log("Force: " + Force);
////            //Debug.Log("Position" + Position);

////            //Velocity += new Vector3(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f));
////            //Velocity += new Vector3(0.01f, -0.01f, 0.01f);
////            //Force += new Vector3(-0.01f, 0.01f, 0.01f);
////            //Force += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

////            //Position = new Vector3(-0.12f, 1.67f, -0.45f);


////            Force = (target - Position) * s;
////            Velocity = (Velocity + Force / m) * d;
////            Position += Velocity;

////            if ((Velocity + Force + Force / m).magnitude < 0.001f)
////            {
////                Position = target;
////            }
////        }
////    }
////}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class JellyMesh : MonoBehaviour
//{
//    public float intensity = 1f;
//    public float mass = 1f;
//    public float stiffness = 1f;
//    public float damping = 0.75f;

//    private Mesh originalMesh;
//    private Mesh meshClone;
//    private MeshRenderer meshRenderer;
//    private JellyVertex[] jellyVertices;
//    private Vector3[] vertexArray;

//    private bool isJellyActive = false;
//    private int jellyCounter = 0;
//    private int startCount = 0;

//    private void Start()
//    {
//        startCount = 1;
//        originalMesh = GetComponent<MeshFilter>().sharedMesh;
//        meshClone = Instantiate(originalMesh);
//        meshRenderer = GetComponent<MeshRenderer>();
//        jellyVertices = new JellyVertex[meshClone.vertices.Length];

//        for (int i = 0; i < meshClone.vertices.Length; i++)
//        {
//            jellyVertices[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
//        }

//        GetComponent<MeshFilter>().sharedMesh = meshClone;
//    }

//    private void FixedUpdate()
//    {
//        if ((isJellyActive && (jellyCounter == 5)) || startCount == 1)
//        {
//            startCount = 0;
//            jellyCounter = 0;
//            Debug.Log("Boing");
//            vertexArray = originalMesh.vertices;

//            for (int i = 0; i < jellyVertices.Length; i++)
//            {
//                Debug.Log("bonk boing");
//                Vector3 target = transform.TransformPoint(vertexArray[jellyVertices[i].ID]);
//                float jellyIntensity = (1 - (meshRenderer.bounds.max.y - target.y) / meshRenderer.bounds.size.y) * intensity;
//                jellyVertices[i].Shake(target, mass, stiffness, damping);
//                target = transform.InverseTransformPoint(jellyVertices[i].Position);
//                vertexArray[jellyVertices[i].ID] = Vector3.Lerp(vertexArray[jellyVertices[i].ID], target, jellyIntensity);
//            }

//            meshClone.vertices = vertexArray;
//        }
//    }

//    private void OnCollisionEnter(Collision other)
//    {
//        Debug.Log("paw and bread collision");
//        jellyCounter += 1;
//        isJellyActive = true;
//    }

//    public class JellyVertex
//    {
//        public int ID;
//        public Vector3 Position;
//        public Vector3 Velocity;
//        public Vector3 Force;

//        public JellyVertex(int id, Vector3 position)
//        {
//            ID = id;
//            Position = position;
//        }

//        public void Shake(Vector3 target, float m, float s, float d)
//        {
//            Force = (target - Position) * s;
//            Velocity = (Velocity + Force / m) * d;
//            Position += Velocity;

//            if ((Velocity + Force + Force / m).magnitude < 0.001f)
//            {
//                Position = target;
//            }
//        }
//    }
//}

