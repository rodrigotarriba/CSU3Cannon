using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGenericTest : MonoBehaviour, IMyGenericTest
{

    //assigns any type of component to a game object, then logs out how many components of that type exist in the object.
    public void CustomAddComponent<T>(GameObject myGameObject)
    {
        //Add the generic type component to the give game object
        myGameObject.AddComponent(typeof(T));

        //obtain the total number of that component in the class
        var arrayOfComponents = myGameObject.GetComponents<T>();
        var numberOfComps = arrayOfComponents.Length;

        //print the component added and the number available in the gameobject
        Debug.Log($"Adding one more \"{typeof(T)}\" for a total of {numberOfComps}");
    }

    private void RunMyMethod()
    {
        //Calling my custom component with Target requires adding the namespace to the file : using CannonApp
        CustomAddComponent<MyGenericTest>(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RunMyMethod", 2f, 5f);
        RunMyMethod();
    }

}
