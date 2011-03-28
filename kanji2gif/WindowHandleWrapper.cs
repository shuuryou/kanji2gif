using System;
using System.Windows.Forms;

namespace Kanji2GIF
{
	internal class WindowHandleWrapper : IWin32Window
	{
		private IntPtr m_hWnd;

		public WindowHandleWrapper(IntPtr hWnd)
		{
			m_hWnd = hWnd;
		}

		public IntPtr Handle
		{
			get { return m_hWnd; }
		}
	}
}
