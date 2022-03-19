using UnityEngine;

namespace UnityCRUD.Scripts.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "UnityCRUDSettings", menuName = "UnityCRUD/Settings", order = 0)]
    public class ServerSettings : ScriptableObject
    {
        public enum DebuglevelEnum
        {
            Verbose = 0,
            Log = 1,
            WarningsAndErrors = 2,
            ErrorsOnly = 3
        }
        [SerializeField] private string serverURL = default;
        public string ServerURL => serverURL;
        [SerializeField] private bool debugLog = true;
        public bool DebugLog => debugLog;
        [SerializeField] private DebuglevelEnum debugLevel = default;
        public DebuglevelEnum Debuglevel => debugLevel;
    }
}