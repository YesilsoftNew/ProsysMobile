using Xamarin.Forms;

namespace WiseMobile.Helper
{
	public interface ISwipeCallBack
	{

		void onLeftSwipe(View view);
		void onRightSwipe(View view);
		void onTopSwipe(View view);
		void onBottomSwipe(View view);
		void onNothingSwiped(View view);
	}
}
