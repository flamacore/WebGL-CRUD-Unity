using TMPro;
using UnityEngine.UI;

namespace UnityCRUD.Scripts.Controllers
{
    public interface IMainTableElementController
    {
        public TextMeshProUGUI ElementBodyText { get; set; }
        public string ElementBodyString { get; set; }
        public Button ElementButton { get; set; }
        public string LayoutElementGuid { get; set; }
        public delegate void OnLoadCompletedDelegate(MainTableElementController controller);
        public event OnLoadCompletedDelegate OnLoadCompleted;
        public delegate void OnInitializeCompletedDelegate(string s);
        public event OnInitializeCompletedDelegate OnInitializeCompleted;
        public delegate void OnButtonClickedDelegate(object objectParam);
        public event OnButtonClickedDelegate OnButtonClicked;
        public bool IsLoaded { get; set; }
        public bool IsInitialized { get; set; }
        enum FieldType
        {
            TextBox,
            Number,
            CheckMark,
            Dropdown
        }
    }
}
