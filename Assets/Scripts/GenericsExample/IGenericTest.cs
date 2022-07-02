using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGenericTest <T>
{
    //we can have the same method with the same name but taking different arguments, one is generic and the other one is not. Called overloads.

    void MyLog(int score);


    //This one takes a generic TValue to give a generic T score. 
    void MyLog<TValue>(T score);


    //When we have 2 parameters, we can have multiple parameters using the same thing. 
    void MyLog<TValue, UValue>(TValue score, UValue score2);


}
