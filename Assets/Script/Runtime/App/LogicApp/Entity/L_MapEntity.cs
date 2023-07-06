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

    L_PropEntity[] propArray;
    public L_PropEntity[] PropArray => propArray;

    L_RoleEntity[] roleArray;
    public L_RoleEntity[] RoleArray => roleArray;

    L_SolidEntity[] solidArray;
    public L_SolidEntity[] SolidArray => solidArray;

    public void Ctor() {

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
