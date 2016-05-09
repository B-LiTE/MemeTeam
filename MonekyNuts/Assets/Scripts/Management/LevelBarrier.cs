using UnityEngine;
using System.Collections;

public class LevelBarrier : MonoBehaviour {

    [SerializeField]
    [Range(2, 3)]
    int destroyAtLevel;

    void Awake()
    {
        References.stateManager.nextLevel += nextLevel;
        ground();
    }

    void Start()
    {
        gameObject.tag = "Terrain";
        gameObject.layer = LayerMask.NameToLayer("Terrain");
    }

    void ground()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, 100000f))
            transform.position = hitInfo.point + new Vector3(0, transform.localScale.y, 0);
        else if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hitInfo, 100000f))
            transform.position = hitInfo.point + new Vector3(0, transform.localScale.y, 0);
    }

    void nextLevel()
    {
        if (References.currentLevel >= destroyAtLevel)
        {
            References.stateManager.nextLevel -= nextLevel;
            if (GetComponent<TreeToBillboard>() != null) References.stateManager.changeState -= GetComponent<TreeToBillboard>().changeLOD;
            Destroy(this.gameObject);
        }
    }

}
