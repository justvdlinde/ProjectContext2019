using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    public static ServiceLocator Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ServiceLocator();
            }
            return Instance;
        }
    }
    private static ServiceLocator instance;

}
