using UnityCRUD.Scripts.Controllers;

namespace UnityCRUD.Scripts.Views
{
    public interface IView
    {
        public void Populate();
        public void NotifyLoadComplete(IMainTableElementController mainTableElementController);
        public void Initialize();
        public delegate void OnInitializeCompletedDelegate();
        public event OnInitializeCompletedDelegate OnInitializeCompleted;
        public delegate void OnButtonClickedDelegate();
        public event OnButtonClickedDelegate OnButtonClicked;
    }
}