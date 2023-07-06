using System.Collections;
using System.Collections.Generic;
using Oshi.Generic;
using Oshi.Template;
using UnityEngine;

public class L_RoleEntity : MonoBehaviour {

    int id;
    public int ID => id;

    int typeID;
    public int TypeID => typeID;

    int dir;
    public int Dir => dir;
    public void SetDir(int value) => dir = value;

    public Vector2Int Pos => transform.position.ToVector2Int();
    public void SetPos(Vector2Int pos) => transform.position = pos.ToVector3();

    public void Ctor() {

    }

    public void FromTM(RoleTM tm) {

        this.typeID = tm.typeID;

    }

    public void FromSpawnTM(RoleSpawnTM tm) {

        transform.position = tm.position.ToVector3();

    }

    public void TearDown() {

        Destroy(gameObject);

    }

}
