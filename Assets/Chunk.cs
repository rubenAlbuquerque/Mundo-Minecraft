using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    
    public Block[,,] chunkdata;
    public GameObject goChunk;
    Material material;
    public enum ChunkStatus { DRAW, DONE };
    public ChunkStatus status;


    public Chunk(Vector3 pos, Material material)
    {
        goChunk = new GameObject(world.CreateChunkName(pos));
        goChunk.transform.position = pos;
        this.material = material;
        BuildChunk();
    }


    void BuildChunk()
    {
        chunkdata = new Block[world.chunkSize, world.chunkSize, world.chunkSize];
        for (int z = 0; z < world.chunkSize; z++)
        {
            for (int y = 0; y < world.chunkSize; y++)
            {
                for (int x = 0; x < world.chunkSize; x++)
                {
                    Vector3 pos = new Vector3(x, y, z);
                    int worldX = (int)goChunk.transform.position.x + x;
                    int worldY = (int)goChunk.transform.position.y + y;
                    int worldZ = (int)goChunk.transform.position.z + z;
                    int h = PerlinTest.GenerateHeight(worldX, worldZ);
                    int hs = PerlinTest.GenerateStoneHeight(worldX, worldZ);

                    if (worldY <= hs)
                    {

                        if (PerlinTest.fBM3D(worldX, worldY, worldZ, 1, 0.5f) < 0.6f)
                            chunkdata[x, y, z] = new Block(Block.BlockType.STONE, pos, this, material);
                        else
                            chunkdata[x, y, z] = new Block(Block.BlockType.AIR, pos, this, material);

                    }
                    else if (worldY == h)
                        chunkdata[x, y, z] = new Block(Block.BlockType.GRASS, pos, this, material);
                    else if (worldY < h)
                        chunkdata[x, y, z] = new Block(Block.BlockType.DIRT, pos, this, material);
                    else
                        chunkdata[x, y, z] = new Block(Block.BlockType.AIR, pos, this, material);


                }
            }
        }

        status = ChunkStatus.DRAW;
    }

    public void DrawChunk()
    { 
        for (int z = 0; z < world.chunkSize; z++)
            for (int y = 0; y < world.chunkSize; y++)
                for (int x = 0; x < world.chunkSize; x++)
                    chunkdata[x, y, z].Draw();

        CombineQuads();
        MeshCollider collider = goChunk.AddComponent<MeshCollider>();
        collider.sharedMesh = goChunk.GetComponent<MeshFilter>().mesh;
        status = ChunkStatus.DONE;
    }

    void CombineQuads()
    {
        // 1. combine all children meshes
        // Coloca todos os meshesfilters num array que vao ser combinados
        MeshFilter[] meshFilters = goChunk.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        //2. Create a new mesh on the parent object
        MeshFilter mf = goChunk.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();

        // 3. Add combined meshes on children as the parent's mesh
        mf.mesh.CombineMeshes(combine);

        // 4. Create a renderer for the parent
        MeshRenderer renderer = goChunk.AddComponent<MeshRenderer>();
        renderer.material = material;

        // 5. Delete all incobind children
        foreach (Transform quad in goChunk.transform)
        {
            GameObject.Destroy(quad.gameObject);
        }


    }

}
