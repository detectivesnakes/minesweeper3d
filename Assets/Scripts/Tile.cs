using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {
    [Header("Tile Types")]
    [SerializeField] private List<Sprite> clicked_tiles;
    [SerializeField] private Texture[] textures;
    private Renderer tileRenderer;
    private int textureIndex;

    [Header("Parents")]
    public Handler gamehandler;
    public bool flagged = false;
    public bool is_mine = false;
    public bool active = true;
    public int mine_count = 0;


    private void Start() {
        tileRenderer = GetComponent<Renderer>();
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("CHECK TILE...");
            ClickedTile();
        }
        else if (Input.GetMouseButtonDown(1)) {
            flagged = !flagged;
            if (flagged) {
                Debug.Log("FLAGGED");
                tileRenderer.material.mainTexture = textures[9];
            }
            else {
                Debug.Log("UNFLAGGED");
                tileRenderer.material.mainTexture = textures[13];
            }
        }
    }

    public void ClickedTile() {
        if (active & !flagged) {
            active = false;
            if (is_mine) {
                Debug.Log("MINE FOUND! PLAYER DEAD");
                tileRenderer.material.mainTexture = textures[12];
                gamehandler.LoseCon();
            } else {
                Debug.Log("CLEAR!");
                if (mine_count == 0) {
                    gamehandler.ClearEmpty(this);
                }
                gamehandler.CheckWinCon();
                tileRenderer.material.mainTexture = textures[mine_count];
            }
        }
    }

    public void Reveal() {
        if (active) {
            active = false;
            if (is_mine & !flagged) {
                tileRenderer.material.mainTexture = textures[10];
            }
            else if (flagged & !is_mine) {
                tileRenderer.material.mainTexture = textures[11];
            }
        }
    }

    public void FlagMine() {
        if (is_mine == true) {
            flagged = true;
            Debug.Log("FLAGGED MINE");
            tileRenderer.material.mainTexture = textures[9];
        }
    }
}
