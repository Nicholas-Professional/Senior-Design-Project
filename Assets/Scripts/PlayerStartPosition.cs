using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{ 
    void Start(){
        GameObject player =GameObject.Find("Team");
        player.transform.position=new Vector3(0,0,0);
        GameObject[] characters=GameObject.FindGameObjectsWithTag("Player");
        float x=0.5f;
        float y=0.5f;
        int a=1;
        for(int i=0;i<characters.Length;i++){
            characters[i].transform.position=new Vector3(x,y,0);
            if(a==1){
                x+=1f;
                a=2;
            }
            else if(a==2){
                y+=1f;
                a=3;
            }
            else if(a==3){
                x-=1f;
            }
        }
        
        player.transform.position=this.transform.position;
        
    }
}
