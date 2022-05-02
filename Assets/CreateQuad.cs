using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

public class CreateQuad : MonoBehaviour
{
    enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK };
    public Material material;
    void Quad(Cubeside side)
    {
        Mesh mesh = new Mesh();

        Vector3 v8 = new Vector3(-0.5f, -0.5f, 0.5f);
        Vector3 v1 = new Vector3(0.5f, -0.5f, 0.5f);
        Vector3 v2 = new Vector3(0.5f, -0.5f, -0.5f);
        Vector3 v3 = new Vector3(-0.5f, -0.5f, -0.5f);
        Vector3 v4 = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 v5 = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 v6 = new Vector3(0.5f, 0.5f, -0.5f);
        Vector3 v7 = new Vector3(-0.5f, 0.5f, -0.5f);

        //Vector3 n = new Vector3(0, 0, 1);
        Vector2 uv00 = new Vector2(0, 0);
        Vector2 uv01 = new Vector2(0, 1);
        Vector2 uv10 = new Vector2(1, 0);
        Vector2 uv11 = new Vector2(1, 1);

        Vector3[] vertices = new Vector3[4];
        Vector3[] normals = new Vector3[4];
        int[] triangles = new int[6];
        Vector2[] uv = new Vector2[4];

        switch (side)
        {
            case Cubeside.FRONT:
                vertices = new Vector3[] { v4, v5, v1, v8 };
                normals = new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward };
                
                break;
            case Cubeside.BOTTOM:
                vertices = new Vector3[] { v8, v1, v2, v3 };
                normals = new Vector3[] { Vector3.down, Vector3.down, Vector3.down, Vector3.down };

                break;

            case Cubeside.TOP:
                vertices = new Vector3[] { v7, v6, v5, v4 };
                normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
                

                break;
            case Cubeside.LEFT:
                vertices = new Vector3[] { v7, v4, v8, v3 };
                normals = new Vector3[] { Vector3.left, Vector3.left, Vector3.left, Vector3.left };

                break;
            case Cubeside.RIGHT:
                vertices = new Vector3[] { v5, v6, v2, v1 };
                normals = new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right };

                break;
            case Cubeside.BACK:
                vertices = new Vector3[] { v6, v7, v3, v2 };
                normals = new Vector3[] { Vector3.back, Vector3.back, Vector3.back, Vector3.back };

                break;

        }
        triangles = new int[] { 3, 1, 0, 3, 2, 1 };
        uv = new Vector2[] { uv11, uv01, uv00, uv10 };

        
        //Vector3[] vertices = new Vector3[] { v4, v5, v1, v0 };
        //Vector3[] normals = new Vector3[] { n, n, n, n };
        //int[] triangles = new int[] { 3, 1, 0, 3, 2, 1 };
        //Vector2[] uv = new Vector2[] { uv11, uv01, uv00, uv10 };
        

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uv;

        GameObject quad = new GameObject("quad");
        quad.transform.parent = this.gameObject.transform;


        //MeshFilter mf = this.gameObject.AddComponent<MeshFilter>();
        MeshFilter mf = quad.AddComponent<MeshFilter>();
        mf.mesh = mesh;

        //MeshRenderer mr = this.gameObject.AddComponent<MeshRenderer>();
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
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
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
        foreach (Transform quad in this.transform)
        {
            Destroy(quad.gameObject);
        }


    }

    void CreateCube()
    {
        Quad(Cubeside.FRONT);
        Quad(Cubeside.BOTTOM);
        Quad(Cubeside.TOP);
        Quad(Cubeside.LEFT);
        Quad(Cubeside.RIGHT);
        Quad(Cubeside.BACK);
        CombineQuads();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ola mundo");
        CreateCube();

        Debug.Log("Ola mundo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



*/


public class CreateQuad : MonoBehaviour
{
    enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK };
    public enum BlockType { GRASS, DIRT, STONE};
    public Material material;
    public BlockType bType;

    static Vector2 GrassSide_LBC = new Vector2(3f, 15f) / 16;
    static Vector2 GrassTop_LBC = new Vector2(2f, 6f) / 16;
    static Vector2 Dirt_LBC = new Vector2(2f, 15f) / 16;
    static Vector2 Stone_LBC = new Vector2(0f, 14f) / 16;

    Vector2[,] blockUVs = {
        {GrassTop_LBC, GrassTop_LBC + new Vector2(1f, 0f)/16, GrassTop_LBC + new Vector2 (0f, 1f)/16, GrassTop_LBC + new Vector2(1f, 1f)/16}, /*GRASS TOP*/ 
        {GrassSide_LBC, GrassSide_LBC + new Vector2(1f, 0f)/16, GrassSide_LBC + new Vector2(0f, 1f)/16, GrassSide_LBC + new Vector2(1f, 1f)/16}, /*GRASS SIDE*/
        {Dirt_LBC, Dirt_LBC + new Vector2(1f, 0f)/16, Dirt_LBC + new Vector2(0f, 1f)/16, Dirt_LBC + new Vector2(1f, 1f)/16},       /*DIRT*/
        {Stone_LBC, Stone_LBC + new Vector2(1f, 0f)/16, Stone_LBC + new Vector2(0f, 1f)/16, Stone_LBC + new Vector2(1f, 1f)/16}    /*STONE*/
    };
    void Quad(Cubeside side)
    {
        
        Mesh mesh = new Mesh();

        Vector3 v8 = new Vector3(-0.5f, -0.5f, 0.5f);
        Vector3 v1 = new Vector3(0.5f, -0.5f, 0.5f);
        Vector3 v2 = new Vector3(0.5f, -0.5f, -0.5f);
        Vector3 v3 = new Vector3(-0.5f, -0.5f, -0.5f);
        Vector3 v4 = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 v5 = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 v6 = new Vector3(0.5f, 0.5f, -0.5f);
        Vector3 v7 = new Vector3(-0.5f, 0.5f, -0.5f);

        //Vector3 n = new Vector3(0, 0, 1);
        Vector2 uv00 = new Vector2(0, 0);
        Vector2 uv01 = new Vector2(0, 1);
        Vector2 uv10 = new Vector2(1, 0);
        Vector2 uv11 = new Vector2(1, 1);

        if (bType == BlockType.GRASS && side == Cubeside.TOP)
        {
            uv00 = blockUVs[0, 0];
            uv10 = blockUVs[0, 1];
            uv01 = blockUVs[0, 2];
            uv11 = blockUVs[0, 3];
        }
        else if (bType == BlockType.GRASS && side == Cubeside.BOTTOM)
        {
            
            uv00 = blockUVs[2, 0];
            uv10 = blockUVs[2, 1];
            uv01 = blockUVs[2, 2];
            uv11 = blockUVs[2, 3];
        }
        else
        {
            uv00 = blockUVs[(int)(bType + 1), 0];
            uv10 = blockUVs[(int)(bType + 1), 1];
            uv01 = blockUVs[(int)(bType + 1), 2];
            uv11 = blockUVs[(int)(bType + 1), 3];
            
        }


            Vector3[] vertices = new Vector3[4];
        Vector3[] normals = new Vector3[4];
        int[] triangles = new int[6];
        Vector2[] uv = new Vector2[4];

        switch (side)
        {
            case Cubeside.FRONT:
                vertices = new Vector3[] { v4, v5, v1, v8 };
                normals = new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward };
                
                break;
            case Cubeside.BOTTOM:
                vertices = new Vector3[] { v8, v1, v2, v3 };
                normals = new Vector3[] { Vector3.down, Vector3.down, Vector3.down, Vector3.down };

                break;

            case Cubeside.TOP:
                vertices = new Vector3[] { v7, v6, v5, v4 };
                normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
                

                break;
            case Cubeside.LEFT:
                vertices = new Vector3[] { v7, v4, v8, v3 };
                normals = new Vector3[] { Vector3.left, Vector3.left, Vector3.left, Vector3.left };

                break;
            case Cubeside.RIGHT:
                vertices = new Vector3[] { v5, v6, v2, v1 };
                normals = new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right };

                break;
            case Cubeside.BACK:
                vertices = new Vector3[] { v6, v7, v3, v2 };
                normals = new Vector3[] { Vector3.back, Vector3.back, Vector3.back, Vector3.back };

                break;

        }
        triangles = new int[] { 3, 1, 0, 3, 2, 1 };
        uv = new Vector2[] { uv11, uv01, uv00, uv10 };

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uv;

        GameObject quad = new GameObject("quad");
        quad.transform.parent = this.gameObject.transform;


        //MeshFilter mf = this.gameObject.AddComponent<MeshFilter>();
        MeshFilter mf = quad.AddComponent<MeshFilter>();
        mf.mesh = mesh;

        //MeshRenderer mr = this.gameObject.AddComponent<MeshRenderer>();
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
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
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
        foreach (Transform quad in this.transform)
        {
            Destroy(quad.gameObject);
        }


    }

    void CreateCube()
    {
        Quad(Cubeside.FRONT);
        Quad(Cubeside.BOTTOM);
        Quad(Cubeside.TOP);
        Quad(Cubeside.LEFT);
        Quad(Cubeside.RIGHT);
        Quad(Cubeside.BACK);
        CombineQuads();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ola mundo");
        CreateCube();

        Debug.Log("Ola mundo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

