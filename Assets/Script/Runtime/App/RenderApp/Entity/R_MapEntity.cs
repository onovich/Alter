using System.Collections;
using System.Collections.Generic;
using Oshi.Template;
using UnityEngine;

public class R_MapEntity : MonoBehaviour {

    int id;
    public int ID => id;

    Vector2Int size;
    public Vector2Int Size => size;

    Vector2 offset;
    public Vector2 Offset => offset;

    R_PropEntity[] propArray;
    public R_PropEntity[] PropArray => propArray;

    R_RoleEntity[] roleArray;
    public R_RoleEntity[] RoleArray => roleArray;

    R_SolidEntity[] solidArray;
    public R_SolidEntity[] SolidArray => solidArray;

    public void Ctor() { }

    public void Init(int id, Vector2Int size, Vector2 offset) {
        this.id = id;
    }

    public void TearDown() {
        Destroy(gameObject);
    }

    public void FromTM(MapTM tm) {
        this.id = tm.id;
        this.size = tm.size;
        this.offset = tm.offset;
    }

}
