package md53975bbd857065843c429f221ab35f1f5;


public class AdMobViewRenderer
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("EZGrader.Droid.AdMobViewRenderer, EZGrader.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdMobViewRenderer.class, __md_methods);
	}


	public AdMobViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == AdMobViewRenderer.class)
			mono.android.TypeManager.Activate ("EZGrader.Droid.AdMobViewRenderer, EZGrader.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public AdMobViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == AdMobViewRenderer.class)
			mono.android.TypeManager.Activate ("EZGrader.Droid.AdMobViewRenderer, EZGrader.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public AdMobViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == AdMobViewRenderer.class)
			mono.android.TypeManager.Activate ("EZGrader.Droid.AdMobViewRenderer, EZGrader.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

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
