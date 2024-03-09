using System.Collections.Generic;
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
                tileRenderer.material.color = Color.blue;
                //spriterenderer.sprite = flagged_tile;
            }
            else {
                Debug.Log("UNFLAGGED");
                tileRenderer.material.color = Color.white;
                //spriterenderer.sprite = unclicked_tile;
            }
        }
    }

    public void ClickedTile() {
        if (active & !flagged) {
            active = false;
            if (is_mine) {
                Debug.Log("MINE FOUND! PLAYER DEAD");
                tileRenderer.material.color = Color.red;
                //spriterenderer.sprite = real_mine;
                gamehandler.LoseCon();
            } else {
                Debug.Log("CLEAR!");
                tileRenderer.material.color = Color.grey;
                //spriterenderer.sprite = clicked_tiles[mine_count];
                if (mine_count == 0) {
                    gamehandler.ClearEmpty(this);
                }
                gamehandler.CheckWinCon();
            }
        }
    }

    public void Reveal() {
        if (active) {
            active = false;
            if (is_mine & !flagged) {
                tileRenderer.material.color = Color.magenta;
                //spriterenderer.sprite = mine_tile;
            }
            else if (flagged & !is_mine) {
                tileRenderer.material.color = Color.yellow;
                //spriterenderer.sprite = wrong_mine;
            }
        }
    }

    public void FlagMine() {
        if (is_mine) {
            flagged = true;
            tileRenderer.material.color = Color.blue;
            //spriterenderer.sprite = flagged_tile;
        }
    }
}
