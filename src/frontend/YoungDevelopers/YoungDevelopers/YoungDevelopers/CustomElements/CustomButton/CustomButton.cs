using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace YoungDevelopers
{
    public class CustomButton : Button
    {
        public int ItemId;
        [Obsolete]
        public static BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create<CustomButton, Xamarin.Forms.TextAlignment>(x => x.HorizontalTextAlignment, Xamarin.Forms.TextAlignment.Center);

        [Obsolete]
        public Xamarin.Forms.TextAlignment HorizontalTextAlignment
        {
            get
            {
                return (Xamarin.Forms.TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            }
            set
            {
                SetValue(HorizontalTextAlignmentProperty, value);
            }
        }

        [Obsolete]
        public static BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create<CustomButton, Xamarin.Forms.TextAlignment>(x => x.VerticalTextAlignment, Xamarin.Forms.TextAlignment.Center);

        [Obsolete]
        public Xamarin.Forms.TextAlignment VerticalTextAlignment
        {
            get
            {
                return (Xamarin.Forms.TextAlignment)GetValue(VerticalTextAlignmentProperty);
            }
            set
            {
                SetValue(VerticalTextAlignmentProperty, value);
            }
        }
    }
}
