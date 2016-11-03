package md5b32d0697ac99619344602a8ddc95568b;


public class Activity_EditarProduto
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("br.com.weblayer.venda.android.Fragments.Activity_EditarProduto, br.com.weblayer.venda.android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Activity_EditarProduto.class, __md_methods);
	}


	public Activity_EditarProduto () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Activity_EditarProduto.class)
			mono.android.TypeManager.Activate ("br.com.weblayer.venda.android.Fragments.Activity_EditarProduto, br.com.weblayer.venda.android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
