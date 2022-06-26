using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace YoungDevelopers
{
    public class DatePickerControl : DatePicker
    {
        public DatePickerControl()
        {
            this.MaximumDate = DateTime.Now.ToUniversalTime();
        }
    }
}
