using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    // Extension methods for the component class
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods#example
    
    public static class ComponentExtension
    {
        public static T GetInterface<T>(this GameObject inObj) where T : class
        {
            return inObj.GetInterfaces<T>().FirstOrDefault();
        }
 
        public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class
        {
            if (!typeof(T).IsInterface) {
                Debug.LogError(typeof(T).ToString() + ": is not an actual interface!");
                return Enumerable.Empty<T>();
            }
 
            return inObj.GetComponents<Component>().OfType<T>();
        }
        
        public static T GetInterfaceInChildren<T>(this GameObject inObj) where T : class
        {
            return inObj.GetInterfacesInChildren<T>().FirstOrDefault();
        }
        
        public static IEnumerable<T> GetInterfacesInChildren<T>(this GameObject inObj) where T : class
        {
            if (!typeof(T).IsInterface) {
                Debug.LogError(typeof(T).ToString() + ": is not an actual interface!");
                return Enumerable.Empty<T>();
            }
 
            return inObj.GetComponentsInChildren<Component>().OfType<T>();
        }
    }
}