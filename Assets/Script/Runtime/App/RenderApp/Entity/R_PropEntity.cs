using System.Collections;
using System.Collections.Generic;
using Oshi.Generic;
using Oshi.Template;
using UnityEngine;

public class R_PropEntity : MonoBehaviour {

    int id;
    public int ID => id;

    int typeID;
    public int TypeID => typeID;

    public Vector2Int Pos => transform.position.ToVector2Int();

    int dir;
    public int Dir => dir;
    public void SetDir(int value) => dir = value;

    public void Ctor() { }

    public void Init(int id, int typeID, Vector2Int pos) { }

    public void FromTM(PropTM tm) {
        this.typeID = tm.typeID;
    }

    public void FromSpawnTM(PropSpawnTM tm) {
        transform.position = tm.position.ToVector3();
    }

    public void TearDown() {
        Destroy(gameObject);
    }

}
