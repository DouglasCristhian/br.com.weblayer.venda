using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.OS;
using br.com.weblayer.venda.android.Helpers;

namespace br.com.weblayer.venda.android.Fragments
{
    [Activity(Label = "Fragment_Legendas")]
    public class Fragment_Legendas : DialogFragment
    {
        public event EventHandler<DialogEventArgs> DialogClosed;
        string Retorno;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Fragment_Legendas, container, false);
            return view;
        }

        public override void OnDismiss(IDialogInterface dialog)
        {
            base.OnDismiss(dialog);
            if (DialogClosed != null)
            {
                DialogClosed(this, new DialogEventArgs { ReturnValue = Retorno });
            }

        }
    }
}