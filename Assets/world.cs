using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class world : MonoBehaviour
{

    public GameObject player;
    public Material material;
    //public static int columnHeight = 4;
    public static int chunkSize = 16;
    public static int radius = 3;
    public static ConcurrentDictionary<string, Chunk> chunkDict;
    public static List<string> toRemove = new List<string>();
    Vector3 lastBuildPos;
    bool drawing;


    public static string CreateChunkName(Vector3 v)
    {
        return (int)v.x + " " + (int)v.y + " " + (int)v.z;
    }


    IEnumerator BuildRecursiveWorld(Vector3 chunkPos, int rad)
    {
        int x = (int)chunkPos.x;
        int y = (int)chunkPos.y;
        int z = (int)chunkPos.z;

        BuildChunkAt(chunkPos);
        yield return null;

        if (--rad < 0) yield break;


        Building(new Vector3(x, y, z + chunkSize), rad);
        yield return null;
        Building(new Vector3(x, y, z - chunkSize), rad);
        yield return null;
        Building(new Vector3(x + chunkSize, y, z), rad);
        yield return null;
        Building(new Vector3(x - chunkSize, y, z), rad);
        yield return null;
        Building(new Vector3(x, y + chunkSize, z), rad);
        yield return null;
        Building(new Vector3(x, y - chunkSize, z), rad);

    }


    void BuildChunkAt(Vector3 chunkPos)
    {
        string name = CreateChunkName(chunkPos);
        Chunk c;
        if (!chunkDict.TryGetValue(name, out c))
        {
            c = new Chunk(chunkPos, material);
            c.goChunk.transform.parent = this.transform;
            chunkDict.TryAdd(c.goChunk.name, c);
        }
    }

    IEnumerator RemoveChunks()
    {
        for (int i = 0; i < toRemove.Count; i++)
        {
            string name = toRemove[i];
            Chunk c;
            if (chunkDict.TryGetValue(name, out c))
            {
                Destroy(c.goChunk);
                chunkDict.TryRemove(name, out c);
                yield return null;
            }
        }
    }



    IEnumerator DrawChunks()
    {
        drawing = true;
        foreach (KeyValuePair<string, Chunk> c in chunkDict)
        {
            if (c.Value.status == Chunk.ChunkStatus.DRAW)
            {
                c.Value.DrawChunk();
                yield return null;
            }
            if (c.Value.goChunk && Vector3.Distance(player.transform.position, c.Value.goChunk.transform.position) > chunkSize * radius)
                toRemove.Add(c.Key);
        
        }
        StartCoroutine(RemoveChunks());
        drawing = false;
    }

    void Building(Vector3 chunkPos, int rad)
    {
        StartCoroutine(BuildRecursiveWorld( chunkPos,  rad));
    }

    void Drawing()
    {
        StartCoroutine(DrawChunks());
    }


    Vector3 WhichChunk(Vector3 position)
    {
        Vector3 chunkPos = new Vector3();
        chunkPos.x = Mathf.Floor(position.x / chunkSize) * chunkSize;
        chunkPos.y = Mathf.Floor(position.y / chunkSize) * chunkSize;
        chunkPos.z = Mathf.Floor(position.z / chunkSize) * chunkSize;
        return chunkPos;


    }


    /*
    IEnumerator BuildWorld()
    {
        for (int x = 0; x < worldSize; x ++)
            for (int z = 0; z < worldSize; z++)
            {
                for (int y = 0; y < columnHeight; y++)
                {
                    Vector3 chunkPos = new Vector3(x* chunkSize, y * chunkSize, z* chunkSize);
                    Chunk c = new Chunk(chunkPos, material);
                    c.goChunk.transform.parent = this.transform;
                    chunkDict.Add(c.goChunk.name, c);
                }
                yield return null;

            }

        foreach (KeyValuePair<string, Chunk> c in chunkDict)
        {
            c.Value.DrawChunk();
            yield return null;


        }
    }
    

    IEnumerator BuildChunkColumn()
    {
        for (int i = 0; i < columnHeight; i++)
        {
            Vector3 chunkPos = new Vector3(this.transform.position.x, i * chunkSize, this.transform.position.z);
            // Criar o chunk
            Chunk c = new Chunk(chunkPos, material);
            c.goChunk.transform.parent = this.transform;
            chunkDict.Add(c.goChunk.name, c);
        }

        foreach (KeyValuePair<string, Chunk> c in chunkDict)
        {
            c.Value.DrawChunk();
            yield return null;
        

        }

    }
    */
    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        chunkDict = new ConcurrentDictionary<string, Chunk>();
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;

        Vector3 ppos = player.transform.position;
        player.transform.position = new Vector3(ppos.x, PerlinTest.GenerateHeight(ppos.x, ppos.z), ppos.z);

        lastBuildPos = WhichChunk(player.transform.position);

        //StartCoroutine(BuildWorld());
        //BuildChunkAt(WhichChunk(player.transform.position));
        //BuildRecursiveWorld(lastBuildPos, radius);
        Building(WhichChunk(lastBuildPos), radius);
        Drawing();
        player.SetActive(true);

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = player.transform.position - lastBuildPos;
        if (movement.magnitude > chunkSize)
        {
            lastBuildPos = player.transform.position;
            Building(WhichChunk(lastBuildPos), radius);
            Drawing();

        }
        if(!drawing)
            Drawing();

    }
}
