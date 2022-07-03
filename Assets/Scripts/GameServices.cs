using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//We make this a static class means it will be accesible everywhere, we need to be careful with static classes since it can be very bad for code if not implemented wisely.
public static class GameServices
{
    //We create a dictionary with an integer and an object, it keeps track of all our services, could be audio, could be player stats, spawning, etc. Keeps track of all the services that are registered
    //Object is the absolute base class of all of C#.net
    //Every .net class is an object
    private static readonly Dictionary<int, object> serviceMap;


    //we create the constructor for this class
    static GameServices()
    {
        //we create a new instance of our variable
        serviceMap = new Dictionary<int, object>();

    }


    //get the id of something in our dictionary (Id is the position)
    private static int GetId<T>()
    {
        //It returns the identifier for Type T
        return typeof(T).GetHashCode();
    }

    
    public static void RegisterService<T>(T service) where T: class
    {
        //It will put the service in a hashcode chosen especifically for that type T
        serviceMap[GetId<T>()] = service;
    }


    public static void DeregisterService<T>(T service) where T:class
    {
        //It will remove the service especific to the hashcode for type T
        serviceMap.Remove(GetId<T>());
    }

    public static T GetService<T>() where T : class
    {
        //It returns the Type T for the service object stored in the dictionary, using the hashcode.
        Debug.Assert(serviceMap.ContainsKey(GetId<T>()), $"{typeof(T)} is not in the dictionary");

        return (T)serviceMap[GetId<T>()];
    }

    public static void Clear()
    {
        serviceMap.Clear();
    }



}
