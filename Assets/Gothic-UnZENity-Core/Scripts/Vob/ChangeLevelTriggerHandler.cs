using GUZ.Core.Manager;
using UnityEngine;

namespace GUZ.Core.Vob
{
    public class ChangeLevelTriggerHandler : MonoBehaviour
    {
        public string levelName;
        public string startVob;
        
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
#pragma warning disable CS4014  // It's intended, that this async call is not awaited.
            GuzSceneManager.I.LoadWorld(levelName, startVob.Trim());
#pragma warning restore CS4014
        }

    }
}
