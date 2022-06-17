using Xamarin.Forms;
using Android.Content;
using YoungDevelopers;
using YoungDevelopers.Droid;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRender))]
namespace YoungDevelopers.Droid
{
    public class CustomPickerRender : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        CustomPicker custompicker = null;
        public CustomPickerRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
            }

            if (e.NewElement != null)
            {
                custompicker = Element as CustomPicker;
                UpdatePickerPlaceholder();
                if (custompicker.SelectedIndex <= -1)
                {
                    UpdatePickerPlaceholder();
                }
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
            if (custompicker != null)
            {
                if (e.PropertyName.Equals(CustomPicker.PlaceholderProperty.PropertyName))
                {
                    UpdatePickerPlaceholder();
                }
            }
        }

        protected override void UpdatePlaceHolderText()
        {
            UpdatePickerPlaceholder();
        }

        void UpdatePickerPlaceholder()
        {
            if (custompicker == null)
                custompicker = Element as CustomPicker;
            if (custompicker.Placeholder != null)
                Control.Hint = custompicker.Placeholder;
        }
    }
}