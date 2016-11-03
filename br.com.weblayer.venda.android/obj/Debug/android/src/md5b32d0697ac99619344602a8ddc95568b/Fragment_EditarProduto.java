package md5b32d0697ac99619344602a8ddc95568b;


public class Fragment_EditarProduto
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("br.com.weblayer.venda.android.Fragments.Fragment_EditarProduto, br.com.weblayer.venda.android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Fragment_EditarProduto.class, __md_methods);
	}


	public Fragment_EditarProduto () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Fragment_EditarProduto.class)
			mono.android.TypeManager.Activate ("br.com.weblayer.venda.android.Fragments.Fragment_EditarProduto, br.com.weblayer.venda.android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
