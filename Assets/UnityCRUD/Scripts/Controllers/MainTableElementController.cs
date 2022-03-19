using TMPro;
using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCRUD.Scripts.Controllers
{
    public class MainTableElementController : MonoBehaviour, IMainTableElementController
    {
        public TextMeshProUGUI ElementBodyText { get; set; }
        public string ElementBodyString { get; set; }
        public Button ElementButton { get; set; }
        public string LayoutElementGuid { get; set; }
        public event IMainTableElementController.OnLoadCompletedDelegate OnLoadCompleted;
        public event IMainTableElementController.OnInitializeCompletedDelegate OnInitializeCompleted;
        public event IMainTableElementController.OnButtonClickedDelegate OnButtonClicked;
        public bool IsLoaded { get; set; }
        public bool IsInitialized { get; set; }
        public Image backgroundImage;
        public IMainTableElementController.FieldType elementFieldType = IMainTableElementController.FieldType.TextBox;

        public virtual void Initialize()
        {
            if (IsInitialized) return;
            IsLoaded = false;
            this.OnLoadCompleted += OnLoadCompleteCallback;
            this.OnInitializeCompleted += OnInitializeCompletedCallback;
            this.OnButtonClicked += OnButtonClickedCallback;
            LayoutElementGuid = Mathf.FloorToInt(Random.value * 100000).ToString();
            CrudLogger.Log("Initializing layout element:" + ElementBodyString);
            ElementBodyText = transform.GetComponentInChildren<TextMeshProUGUI>();
            ElementButton = transform.GetComponent<Button>();
            if (ElementButton) ElementButton.interactable = false;
            if (ElementBodyText) ElementBodyText.enabled = false;
            OnInitializeCompleted?.Invoke(ElementBodyString);
            IsInitialized = true;
        }

        public void OnInitializeCompletedCallback(string bodyText)
        {
            CrudLogger.Log("Initialize completed for layout element:" + ElementBodyString);
            if (ElementBodyText) ElementBodyText.text = bodyText;
            if (ElementButton) ElementButton.onClick.AddListener(OnButtonClick);
            OnLoadCompleted?.Invoke(this);
        }

        protected virtual void OnButtonClickedCallback(object objectParam)
        {
            if (UiElementsDataContainer.IsInputLocked) return;
        }

        public void OnLoadCompleteCallback(MainTableElementController controller)
        {
            if (ElementButton) ElementButton.interactable = true;
            if (ElementBodyText) ElementBodyText.enabled = true;
            IsLoaded = true;
        }

        public void OnButtonClick()
        {
            OnButtonClicked?.Invoke(null);
        }

        private void OnDisable()
        {
            this.OnLoadCompleted -= OnLoadCompleteCallback;
            this.OnInitializeCompleted -= OnInitializeCompletedCallback;
            this.OnButtonClicked -= OnButtonClickedCallback;
            if (ElementButton) ElementButton.onClick.RemoveListener(OnButtonClick);
        }
    }
}
