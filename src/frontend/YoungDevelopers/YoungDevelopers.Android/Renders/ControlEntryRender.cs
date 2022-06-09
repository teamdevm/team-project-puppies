using Android.Content;
using Android.Content.Res;
using YoungDevelopers;
using YoungDevelopers.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.OS;
using Android.Graphics;

[assembly: ExportRenderer(typeof(ControlEntry), typeof(ControlEntryRender))]
namespace YoungDevelopers.Droid
{
    class ControlEntryRender : EntryRenderer
    {
        public ControlEntryRender(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (e.NewElement is ControlEntry NewControlEntry)
            {
                // Tint Color
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Control.BackgroundTintList = ColorStateList.ValueOf(NewControlEntry.MyTintColor.ToAndroid());
                }
                else
                {
                    Control.Background.SetColorFilter(new PorterDuffColorFilter(NewControlEntry.MyTintColor.ToAndroid(), PorterDuff.Mode.SrcAtop));
                }


                // Highlight Color
                Control.SetHighlightColor(color: NewControlEntry.MyHighlightColor.ToAndroid());
                Control.Background = null;
                Control.SetBackgroundColor(color: Android.Graphics.Color.Transparent);
            }

        }

    }
}