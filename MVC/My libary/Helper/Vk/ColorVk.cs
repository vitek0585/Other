namespace MvcRegionCity.Extensions
{

    public struct ColorVk
    {
        private string _colorWidget;

        public string ColorWidget
        {
            get { return _colorWidget ?? "FFFFFF"; }
            set { _colorWidget = value; }
        }
        private string _colorText;
        public string ColorText
        {
            get { return _colorText ?? "2B587A"; }
            set { _colorText = value; }
        }
        private string _colorButton;

        public string ColorButton
        {
            get { return _colorButton ?? "5B7FA6"; }
            set { _colorButton = value; }
        }
       
        /// <param name="colorWidget">цвет фона виджета в формате RRGGBB</param>
        /// <param name="colorText">цвет текста в формате RRGGBB</param>
        /// <param name="colorButton">цвет кнопок в формате RRGGBB</param>
        public ColorVk(string colorWidget, string colorText, string colorButton)
            : this()
        {
            ColorWidget = colorWidget;
            ColorText = colorText;
            ColorButton = colorButton;
        }

        
        
    }
}