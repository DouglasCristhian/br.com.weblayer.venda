using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace br.com.weblayer.venda.android.Activities
{
    public abstract class Activity_Base : AppCompatActivity
    {
        public Toolbar Toolbar
        {
            get;
            set;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(LayoutResource);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (Toolbar != null)
            {
                //Toolbar.InflateMenu(Resource.Menu.menu_toolbarvazia);
                SetSupportActionBar(Toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        protected abstract int LayoutResource
        {
            get;
        }

        //protected abstract int MenuResource
        //{
        //    get;
        //}

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(MenuResource, menu);
        //    return true;
        //}

        //protected int ActionBarIcon
        //{
        //    set { Toolbar.SetNavigationIcon(value); }
        //}

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();

                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}