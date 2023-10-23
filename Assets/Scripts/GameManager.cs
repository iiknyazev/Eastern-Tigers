using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int LevelNumber { get; private set; }
    public static bool IsPaused { get; private set; }

    public static EventHandler OnPuzzleAssembled;

    private List<GameObject> shards;

    private void Awake()
    {
        LevelNumber = int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]);
    }

    void Start()
    {
        IsPaused = false;

        UpdateListShards();

        LevelUIScript.OnSettingsShown += (object sender, EventArgs e) => IsPaused = true;
        LevelUIScript.OnSettingsHidden += (object sender, EventArgs e) => IsPaused = false;
    }

    private void UpdateListShards()
    {
        shards = new List<GameObject>();

        for (int i = 1; i <= 10; i++)
        {
            GameObject shard = GameObject.Find("Pic " + LevelNumber + " shard " + i);
            if (shard != null)
                shards.Add(shard);
        }
    }

    void Update()
    {
        if (IsLevelComplete())
        {
            OnPuzzleAssembled?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene("Level_" + (LevelNumber % 4 + 1));
        }
    }

    private bool IsLevelComplete()
    {
        if (shards.Count == 0)
            throw new System.Exception("No shards were found on the scene");

        foreach (GameObject shard in shards)
            if (!shard.GetComponent<ShardScript>().IsPlaced)
                return false;

        return true;
    }
}
