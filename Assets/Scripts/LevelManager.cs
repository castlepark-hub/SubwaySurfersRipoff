using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class generates in the level while in game. 
/// </summary>
public class LevelManager : MonoBehaviour
{
    public List<LevelChunk> chunks;
    public Transform player;
    public float generationDistance = 100; // how far ahead
    public float deleteDistance = 50; // how far behind
    public float startPos = 10; 


    float nextLevelPos;
    List<LevelChunk> generatedChunks = new List<LevelChunk>();

    void Start()
    {
        nextLevelPos = startPos; 
        GameManager.OnStartGame += ResetLevel; 
    }

	void OnDestroy()
	{
        GameManager.OnStartGame -= ResetLevel; 
    }

	// Update is called once per frame
	void Update()
    {
        // spawn chunks in front
        while (nextLevelPos < player.position.z + generationDistance)
        {
            NewLevelChunk();
        }

        // delete chunks behind
        var lastChunkPos = generatedChunks[0].transform.position.z + generatedChunks[0].levelLength;
        if (lastChunkPos + deleteDistance < player.position.z)
		{
            Destroy(generatedChunks[0].gameObject);
            generatedChunks.RemoveAt(0);
        }
    }

    void ResetLevel()
	{
        nextLevelPos = startPos;

		for (int i = 0; i < generatedChunks.Count; i++)
		{
            Destroy(generatedChunks[i].gameObject);
        }

        generatedChunks.Clear();
    }

    void NewLevelChunk()
	{
        int chunkNumber = Random.Range(0, chunks.Count);
        LevelChunk chunk = Instantiate(chunks[chunkNumber],
            new Vector3(0, 0, nextLevelPos), Quaternion.identity, transform);
        float chunkLength = chunk.levelLength;
        nextLevelPos += chunkLength;
        generatedChunks.Add(chunk);
    }
}
