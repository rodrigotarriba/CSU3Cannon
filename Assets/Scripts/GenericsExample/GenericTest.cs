using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CannonApp; //added the namespace to be able to import Target script component.

/// <summary>
/// This is a generic class that allows for any type T of value to be assigned to the thing being carried on.
/// </summary>

public class GenericTest : MonoBehaviour, IGenericTest <int>
{
    //still in the works
    private void Awake()
    {
        //MyLog(10); //Generic01
        //MyLog<float>(10); //Generic02
        //MyLog<float, int>(10.5f, 3); //Generic03
        //MyLog<Transform, int>(transform, 10); //Generics03
        LogComponent(transform);
        LogComponent(GetComponent<MeshRenderer>());
        LogComponent<Renderer>(GetComponent<MeshRenderer>());
        
        

    }

    public void MyLog(int score)
    {
        Debug.Log($"Generics01: {score}.");
    }


    public void MyLog<TValue>(int score)
    {
        Debug.Log($"Generics02: The method is of type \"{typeof(TValue)}\", \nthe score is of type {typeof(int)}, for a value of {score}.");
    }


    public void MyLog<TValue, UValue>(TValue score, UValue score2)
    {
        Debug.Log($"Generics03: {score}, {score2}.");
    }



    //We want T to be a monobehaviour so we do T:MonoBehaviour
    public void CustomAddComponent<T>(GameObject gameObject) where T: MonoBehaviour 
    {
        var component = gameObject.gameObject.AddComponent<T>();
        component.enabled = false;

        Debug.Log($"Generics Test {typeof(T)} \n {gameObject.GetComponents<T>().Length}");

    }

    //This one prints the type of T where T could be ANY component
    private void LogComponent<T>(T component) where T : Component
    {
        Debug.Log($"Generics \"LogComponent\" {typeof(T)} | {component}");
    }


}
