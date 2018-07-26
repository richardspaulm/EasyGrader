using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EZGrader
{
    public class EntryExt : Entry
    {
        // Need to overwrite default handler because we cant Invoke otherwise
        public new event EventHandler Completed;

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
            nameof(ReturnType),
            typeof(ReturnType),
            typeof(EntryExt),
            ReturnType.Done,
            BindingMode.OneWay
        );

        public ReturnType ReturnType
        {
            get { return (ReturnType)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }
    }

    // Not all of these are support on Android, consult EntryEditText.ImeOptions
    public enum ReturnType : int
    {
        Go,
        Next,
        Done,
        Send,
        Search
    }
}
