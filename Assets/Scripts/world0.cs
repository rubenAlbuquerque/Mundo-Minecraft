using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject block;
    public int size;

    void BuildWorld()
    {
        for (int z = 0; z < size; z++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Vector3 pos = new Vector3(x, y, z);
                    GameObject cube = GameObject.Instantiate(block, pos, Quaternion.identity);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BuildWorld();


    }

    // Update is called once per frame
    void Update()
    {

    }
}


/* 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int size;
    public GameObject block;

    void BuildWorld(){
        for (int z = 0; z < size; z++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Vector3 pos = new Vector3(x, y, z);
                    GameObject cube = GameObject.Instantiate(block, pos, Quaternion.identity);
                    cube.transform.parent = this.transform;
                    cube.name = x + " " + y + " " + z;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ola mundo");
        BuildWorld();
        Debug.Log("Fim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/



/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world : MonoBehaviour
{
    enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK}
    public Material material;

    void Quad(Cubeside side)
    {
        Mesh mesh = new Mesh();
        Vector3 v0 = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 v1 = new Vector3(0.5f, -0.5f, 0.5f);
        Vector3 v2 = new Vector3(0.5f, -0.5f, -0.5f);
        Vector3 v3 = new Vector3(-0.5f, -0.5f, -0.5f);
        Vector3 v4 = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 v5 = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 v6 = new Vector3(0.5f, 0.5f, -0.5f);
        Vector3 v7 = new Vector3(-0.5f, 0.5f, -0.5f);

        Vector3 n = new Vector3(0, 0, 1);
        Vector3 uv00 = new Vector2(0, 0);
        Vector3 uv01 = new Vector2(0, 1);
        Vector3 uv10 = new Vector2(1, 0);
        Vector3 uv11 = new Vector2(1, 1);

        Vector3[] vertices = new Vector3[4];
        Vector3[] normals = new Vector3[4];
        int[] triangles = new int[6];
        Vector2[] uv = new Vector2[4];

        switch (side)
        {
            case Cubeside.FRONT:
                vertices = new Vector3[] { v4, v5, v1, v0 };
                normals = new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward};
                triangles = new int[] { 3, 1, 0, 3, 2, 1 };
                uv = new Vector2[] { uv11, uv01, uv00, uv10 };
                break;
        }

        GameObject quad = new GameObject("quad");
        quad.transform.parent = this.gameObject.transform;

        //Vector3 vertices = new Vector3[] { v4, v5, v1, v0 };
        //Vector3 normals = new Vector3[] { n, n, n, n };
        triangles = new int[] { 3, 1, 0, 3, 2, 1 };
        uv = new Vector2[] { uv11, uv01, uv00, uv10 };

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uv;

        MeshFilter mf = quad.AddComponent<MeshFilter>();
        mf.mesh = mesh;

        //MeshRenderer mr = quad.AddComponent<MeshRenderer>();
        //mr.material = material;
        


    }
    
    void CombineQuads()
    {
        // 1. combine all children meshes
        // Coloca todos os meshesfilters num array que vao ser combinados
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while(i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].shareMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        //2. Create a new mesh on the parent object
        MeshFilter mf = this.gameObject.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();

        // 3. Add combined meshes on children as the parent's mesh
        mf.mesh.CombineMeshes(combine);

        // 4. Create a renderer for the parent
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        GetComponent<Renderer>().material = material;

        // 5. Delete all incobind children
        foreach(Transform quad in this.transform)
        {
            Destroy(quad.gameObject);
        }


    }
    


    void createcube()
    {
        Quad(Cubeside.LEFT);
        Quad(Cubeside.RIGHT);
        Quad(Cubeside.BACK);
        Quad(Cubeside.BOTTOM);
        Quad(Cubeside.FRONT);
        Quad(Cubeside.TOP);
        //CombineQuads();


    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("OLa mundo");
        print("adeus");
        Quad(Cubeside side)
        //createcube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
