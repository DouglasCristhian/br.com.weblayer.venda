package md5ecc2ac2a7298ebcc3325b787522723bd;


public class Activity_Pedidos
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
		mono.android.Runtime.register ("br.com.weblayer.venda.android.Activities.Activity_Pedidos, br.com.weblayer.venda.android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Activity_Pedidos.class, __md_methods);
	}


	public Activity_Pedidos () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Activity_Pedidos.class)
			mono.android.TypeManager.Activate ("br.com.weblayer.venda.android.Activities.Activity_Pedidos, br.com.weblayer.venda.android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
