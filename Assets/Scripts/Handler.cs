using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class Handler : MonoBehaviour {
    [SerializeField] private Transform Tile;
    [SerializeField] private Transform GameHolder;
    [SerializeField] GameObject gameover;
    [SerializeField] GameObject mainui;

    private List<Tile> TileBank = new();
    public string difficulty = ("");
    public int width;
    public int height;
    public int num_mines;
    public float size = 1f;

    public void Start()
    {
        mainui = GameObject.Find("MainUI");
        gameover = GameObject.Find("GameOver");
        gameover.SetActive(false);
    }

    public void NewGame(int width, int height, int num_mines) {
        this.width = width;
        this.height = height;
        this.num_mines = num_mines;

        for (int i = 0; i < height; i++) {      // row
            for (int j = 0; j < width; j++) {   // col
                Transform newtile = Instantiate(Tile);
                newtile.parent = GameHolder;
                float x = j - ((width - 1) / 2.0f);
                float z = i - ((height - 1) / 2.0f);
                newtile.localPosition = new Vector3(x*size, 1, z*size);

                Tile tile = newtile.GetComponent<Tile>();
                TileBank.Add(tile);
                tile.gamehandler = this;
            }
        }
    }

    public void ResetBoard() {
        int[] minetiles = Enumerable.Range(0, TileBank.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();

        for (int i = 0; i < num_mines; i++) {
            int pos = minetiles[i];
            TileBank[pos].is_mine = true;
        }

        for (int i = 0; i < TileBank.Count; i++) {
            TileBank[i].mine_count = CountMines(i);
        }
    }

    private int CountMines(int tile) {
        int inc = 0;
        foreach (int element in Radar(tile)) {
            if (TileBank[element].is_mine) {
                inc++;
            }
        }
        return inc;
    }

    private List<int> Radar(int pos) {
        List<int> localmines = new();
        int row = pos / width;
        int col = pos % width;

        if (row < (height - 1)) {
            localmines.Add(pos + width);
            if (col > 0) {
                localmines.Add(pos + width - 1);
            }
            if (col < (width - 1)) {
                localmines.Add(pos + width + 1);
            }
        }
        if (col > 0) {
            localmines.Add(pos - 1);
        }
        if (col < (width - 1)) {
            localmines.Add(pos + 1);
        }
        if (row > 0) {
            localmines.Add(pos - width);
            if (col > 0) {
                localmines.Add(pos - width - 1);
            }
            if (col < (width - 1)) {
                localmines.Add(pos - width + 1);
            }
        }
        return localmines;
    }

    public void ClearEmpty(Tile tile) {
        int pos = TileBank.IndexOf(tile);
        foreach (int element in Radar(pos)) {
            TileBank[element].ClickedTile();
        }
    }

    public void LoseCon() {
        foreach (Tile tile in TileBank){
            tile.Reveal();
        }
        mainui.SetActive(false);
        gameover.SetActive(true);
        StartCoroutine(DelayLoss());
        StopCoroutine(DelayLoss());
    }

    public void CheckWinCon() {
        int inc = 0;
        foreach (Tile tile in TileBank) {
            if (tile.active) {
                inc++;
            }
        }
        if (inc == num_mines) {
            Debug.Log("Congratulations! You win.");
            foreach (Tile tile in TileBank) {
                tile.active = false;
                tile.FlagMine();
            }
        }
    }

    IEnumerator DelayLoss() {
        Debug.Log("Coroutine started " + Time.time);
        yield return new WaitForSeconds(3.0f);
        gameover.SetActive(false);
        for (var i = GameHolder.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(GameHolder.transform.GetChild(i).gameObject);
        }
        yield return new WaitForSeconds(.1f);
        mainui.SetActive(true);
    }
}
