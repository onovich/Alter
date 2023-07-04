using System.Collections;
using System.Collections.Generic;
using Oshi.Template;
using UnityEngine;

public class L_SolidEntity : MonoBehaviour {

    int id;
    public int ID => id;

    int typeID;
    public int TypeID => typeID;

    Vector2Int pos;
    public Vector2Int Pos => pos;
    public void SetPos(Vector2Int value) => pos = value;

    int dir;
    public int Dir => dir;
    public void SetDir(int value) => dir = value;

    public void Ctor() {

    }

    public void FromTM(SolidTM tm) {
    }

    public void TearDown() {
        Destroy(gameObject);
    }

}