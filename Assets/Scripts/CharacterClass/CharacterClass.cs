using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass
{
    public int basicHealth=10;
    public int basicMovement=3;

    public int getHealth(){
        return basicHealth;
    }
    public int getMovement(){
        return basicMovement;
    }
}
