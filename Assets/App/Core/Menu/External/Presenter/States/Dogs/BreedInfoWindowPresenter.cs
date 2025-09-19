using App.Menu.UI.External.Presenter.Dto;
using App.Menu.UI.External.View.Dogs;

namespace App.Menu.UI.External.Presenter
{
    public class BreedInfoWindowPresenter
    {
        private readonly BreedInfoWindow m_View;

        public BreedInfoWindowPresenter(BreedInfoWindow view)
        {
            m_View = view;
        }

        public void Initialize()
        {
            m_View.SetOkButtonClickCallback(OnOkClick);
        }

        private void OnOkClick()
        {
            m_View.SetActive(false);
        }

        public void SetActive(bool status)
        {
            m_View.SetActive(status);
        }

        public void OpenWindow(FactsAttributesDto facts)
        {
            m_View.SwitchStateByHeight.SetLessState(); 
            SetActive(true); 
            SetInfo(facts); 
            m_View.SwitchStateByHeight.UpdateState(); 
        }

        public void SetInfo(FactsAttributesDto facts)
        {
            m_View.SetNameText(facts.Name);
            m_View.SetDescriptionText(facts.Description);
        }
    }
}