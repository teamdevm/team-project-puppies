using Xamarin.Forms;

namespace YoungDevelopers
{
    public class ControlEntry : Entry
    {
        #region Bindable Properties
        public static readonly BindableProperty MyHighlightColorProperty = BindableProperty.Create(nameof(MyHighlightColor), typeof(Color), typeof(ControlEntry));

        public static readonly BindableProperty MyHandleColorProperty = BindableProperty.Create(nameof(MyHandleColor), typeof(Color), typeof(ControlEntry));

        public static readonly BindableProperty MyTintColorProperty = BindableProperty.Create(nameof(MyTintColor), typeof(Color), typeof(ControlEntry));
        #endregion

        #region Properties
        public Color MyHighlightColor
        {
            get => (Color)GetValue(MyHighlightColorProperty);
            set => SetValue(MyHighlightColorProperty, value);
        }
        public Color MyHandleColor
        {
            get => (Color)GetValue(MyHandleColorProperty);
            set => SetValue(MyHandleColorProperty, value);
        }
        public Color MyTintColor
        {
            get => (Color)GetValue(MyTintColorProperty);
            set => SetValue(MyTintColorProperty, value);
        }
        #endregion
    }
}