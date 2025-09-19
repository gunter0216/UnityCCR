using System;
using App.Core.Menu.External.Presenter.States.Dogs.Dto.Breeds;
using App.Core.Menu.External.View.Dogs;

namespace App.Core.Menu.External.Presenter.States.Dogs
{
    public class BreedPresenter
    {
        private readonly BreedView m_View;
        private event Action<BreedPresenter> m_OnClickCallback;

        private BreedDto m_Breed;

        public BreedDto Breed => m_Breed; 

        public BreedPresenter(BreedView view, Action<BreedPresenter> onClickCallback)
        {
            m_View = view;
            m_OnClickCallback = onClickCallback;
        }
        
        public void Initialize()
        {
            m_View.SetButtonClickCallback(OnButtonClick);
        }

        private void OnButtonClick()
        {
            m_OnClickCallback?.Invoke(this);
        }

        public void SetBreed(BreedDto breedDto, int index)
        {
            m_Breed = breedDto;
            m_View.SetNameText(m_Breed.Attributes.Name);
            m_View.SetNumberText((index + 1).ToString());
            m_View.SetDownloadIconActive(false);
            SetAsLastSibling();
            SetLoadDownloadIconActive(false);
        }

        public void SetActive(bool status)
        {
            m_View.SetActive(status);
        }

        public void SetAsLastSibling()
        {
            m_View.SetAsLastSibling();
        }

        public void SetLoadDownloadIconActive(bool status)
        {
            m_View.SetDownloadIconActive(status);
        }
    }
}