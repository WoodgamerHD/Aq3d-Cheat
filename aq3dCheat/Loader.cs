using UnityEngine;
using System.Runtime.InteropServices;


namespace Aq3dCheat
{
    public class Loader : MonoBehaviour
    {

        public static GameObject _loadObject;


        public static void Load()
        {

            _loadObject = new GameObject();

            _loadObject.AddComponent<Main>();



            Object.DontDestroyOnLoad(_loadObject);
        }

        public static void Unload()
        {
            _Unload();
        }

        public static void _Unload()
        {
            GameObject.Destroy(_loadObject);
        }


    }
}
