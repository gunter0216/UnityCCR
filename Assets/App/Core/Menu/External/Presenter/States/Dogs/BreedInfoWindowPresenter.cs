using App.Core.Menu.External.Presenter.States.Dogs.Dto.Facts;
using App.Core.Menu.External.View.Dogs;

namespace App.Core.Menu.External.Presenter.States.Dogs
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