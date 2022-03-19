using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Views;

namespace UnityCRUD.Scripts.Controllers
{
    public class CellTextElementController : MainTableElementController
    {
        public ColumnDataContainer dataContainer;
        public string tableColumnName = default;
        public int rowId = default;
        public override void Initialize()
        {
            if (IsInitialized) return;
            ElementBodyString = dataContainer.columnName;
            backgroundImage.color = MainView.CurrentColor;
            base.Initialize();
        }
        
        protected override void OnButtonClickedCallback(object objectParam)
        {
            base.OnButtonClickedCallback(objectParam);
            PopupView.Instance.Initialize();
            PopulatePopup();
        }

        public void PopulatePopup()
        {
            PopupView.Instance.InitializeField(elementFieldType, this, tableColumnName, rowId);
        }
    }
}