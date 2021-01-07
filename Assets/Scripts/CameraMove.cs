using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CameraMove : MonoBehaviour
{
    float mainSpeed = 20.0f;
    public Tilemap tilemap;

    BoundsInt bounds;

    private float totalRun=1.0f;

    void Start(){
        tilemap.CompressBounds();
        bounds=tilemap.cellBounds;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 p = GetBaseInput();
        totalRun=Mathf.Clamp(totalRun*0.5f,1f,1000f);
        p=p*mainSpeed;

        p=p*Time.deltaTime;

        Vector3 newPosition=transform.position;

        if(newPosition.x+p.x <bounds.xMin+3 || newPosition.x+p.x>bounds.xMax-3){
            p.x=0;
        }
        if(newPosition.y+p.y<bounds.yMin+.5 || newPosition.y+p.y>bounds.yMax-.5){
            p.y=0;
        }

        transform.Translate(p);
    }


    private Vector3 GetBaseInput() {
        Vector3 whereTo = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            whereTo += new Vector3(0, 1 , 0);
        }
        if (Input.GetKey (KeyCode.S)){
            whereTo += new Vector3(0, -1, 0);
        }
        if (Input.GetKey (KeyCode.A)){
            whereTo += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            whereTo += new Vector3(1, 0, 0);
        }
        return whereTo;
    }
}
