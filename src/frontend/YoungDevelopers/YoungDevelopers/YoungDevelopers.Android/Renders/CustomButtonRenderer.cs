using Android.Views;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamrinTemp.Droid;
using YoungDevelopers;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace XamrinTemp.Droid
{
    [System.Obsolete]
    public class CustomButtonRenderer : ButtonRenderer
    {
        public new CustomButton Element
        {
            get
            {
                return (CustomButton)base.Element;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                return;
            }

            SetHorizonalTextAlignment();
            SetVerticalTextAlignment();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomButton.HorizontalTextAlignmentProperty.PropertyName)
            {
                SetHorizonalTextAlignment();
            }

            if (e.PropertyName == CustomButton.VerticalTextAlignmentProperty.PropertyName)
            {
                SetVerticalTextAlignment();
            }
        }

        private void SetHorizonalTextAlignment()
        {
            Control.Gravity = Element.HorizontalTextAlignment.ToHorizontalGravityFlags() | Element.VerticalTextAlignment.ToVerticalGravityFlags();
        }

        private void SetVerticalTextAlignment()
        {
            Control.Gravity = Element.VerticalTextAlignment.ToVerticalGravityFlags() | Element.HorizontalTextAlignment.ToHorizontalGravityFlags();
        }
    }

    public static class AlignmentHelper
    {
        public static GravityFlags ToHorizontalGravityFlags(this Xamarin.Forms.TextAlignment alignment)
        {
            if (alignment == Xamarin.Forms.TextAlignment.Center)
                return GravityFlags.CenterHorizontal;
            return alignment == Xamarin.Forms.TextAlignment.End ? GravityFlags.Right : GravityFlags.Left;
        }

        public static GravityFlags ToVerticalGravityFlags(this Xamarin.Forms.TextAlignment alignment)
        {
            if (alignment == Xamarin.Forms.TextAlignment.Center)
                return GravityFlags.CenterVertical;
            return alignment == Xamarin.Forms.TextAlignment.End ? GravityFlags.Top : GravityFlags.Bottom;
        }
    }
}