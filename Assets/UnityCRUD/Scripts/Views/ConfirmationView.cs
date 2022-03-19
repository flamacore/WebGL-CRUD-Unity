using System;
using UnityCRUD.Scripts.Controllers;

namespace UnityCRUD.Scripts.Views
{
    public class ConfirmationView : BaseView<ConfirmationView>, IView
    {
        public static Action AcceptedAction;
        public override void ButtonClicked()
        {
            base.ButtonClicked();
            CrudLogger.Log("ConfirmationButtonClicked");
            AcceptedAction.Invoke();
        }
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public override void Initialize()
        {
            base.Initialize();
            gameObject.SetActive(true);
        }
    }
}