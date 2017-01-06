using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Ajuda")]
    public class Activity_Ajuda : Activity_Base
    {
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Ajuda;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ImageView img;
            img = FindViewById<ImageView>(Resource.Id.Weblayer_Logo);
            img.SetBackgroundResource(Resource.Drawable.Weblayer_Logo);
        }
    }
}