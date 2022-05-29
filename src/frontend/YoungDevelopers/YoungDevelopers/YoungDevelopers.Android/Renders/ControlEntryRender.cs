using Android.Content;
using Android.Content.Res;
using YoungDevelopers;
using YoungDevelopers.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;


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

            Control.BackgroundTintList = ColorStateList.ValueOf(NewControlEntry.MyTintColor.ToAndroid());
            //Control.HighlightColor = 
        }
    }
}