using System.Web.Mvc;

namespace CRS.Models
{
    public class ModalViewModel
    {
        #region Fields

        private string _defaultButtonText = "Save";
        private string _cancelButtonText = "Cancel";
        private bool _hasCloseButton = true;
        private bool _hasCancelButton = true;
        private bool _hasDefaultButton = true;

        #endregion


        #region Properties

        public object Attributes { get; set; }

        public MvcHtmlString Body { get; set; }

        public object BodyAttributes { get; set; }

        public object CancelButtonAttributes { get; set; }

        public string CancelButtonText
        {
            get
            {
                return this._cancelButtonText;
            }
            set
            {
                this._cancelButtonText = value;
            }
        }

        public object DefaultButtonAttributes { get; set; }

        public string DefaultButtonText
        {
            get
            {
                return this._defaultButtonText;
            }
            set
            {
                this._defaultButtonText = value;
            }
        }

        public MvcHtmlString Footer { get; set; }

        public object FooterAttributes { get; set; }

        public bool HasCancelButton
        {
            get
            {
                return this._hasCancelButton;
            }
            set
            {
                this._hasCancelButton = value;
            }
        }

        public bool HasCloseButton
        {
            get
            {
                return this._hasCloseButton;
            }
            set
            {
                this._hasCloseButton = value;
            }
        }

        public bool HasDefaultButton
        {
            get
            {
                return this._hasDefaultButton;
            }
            set
            {
                this._hasDefaultButton = value;
            }
        }

        public object HeaderAttributes { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public object TitleAttributes { get; set; }

        #endregion


        #region Constructor

        public ModalViewModel(string name)
        {
            this.Name = name;
        }

        #endregion
    }
}