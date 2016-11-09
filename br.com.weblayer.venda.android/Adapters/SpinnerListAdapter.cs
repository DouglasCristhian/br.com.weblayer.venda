using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace br.com.weblayer.venda.android.Adapters
{
    public class SpinnerListAdapter : BaseAdapter<string>
    {
        public List<string> mList;
        private Context mContext;

        public SpinnerListAdapter(Context context, List<string> items)
        {
            mList = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                return mList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get
            {
                return mList[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.SpinnerListAdapter, null, false);
            }

            Spinner spin = row.FindViewById<Spinner>(Resource.Id.item1);

            return row;
        }
    }
}