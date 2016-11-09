using Android.App;
using Android.OS;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Ajuda")]
    public class Activity_Ajuda : Activity_Base
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Ajuda);
        }
    }
}