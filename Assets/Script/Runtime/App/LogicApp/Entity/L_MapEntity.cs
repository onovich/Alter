using System.Collections;
using System.Collections.Generic;
using Oshi.Template;
using UnityEngine;

public class L_MapEntity : MonoBehaviour {

    int id;
    public int ID => id;

    Vector2Int size;
    public Vector2Int Size => size;

    Vector2 offset;
    public Vector2 Offset => offset;

    public List<L_PropEntity> entitySlot;

    public void Ctor() {
        entitySlot = new List<L_PropEntity>();
    }

    public void FromTM(MapTM tm) {
        this.id = tm.id;
        this.size = tm.size;
        this.offset = tm.offset;
    }

    public void TearDown() {
        Destroy(gameObject);
    }

}
