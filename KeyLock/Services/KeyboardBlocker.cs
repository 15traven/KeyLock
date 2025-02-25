using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using KeyLock.Contracts;

namespace KeyLock.Services;

public class KeyboardBlocker : IKeyboardBlocker
{

    private const int WH_KEYBOARD_LL = 13;
    private delegate IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr hookID = IntPtr.Zero;
    private static KeyboardHookProc proc = HookCallback;

    public void BlockKeyboard()
    {
        if (hookID == IntPtr.Zero)
        {
            hookID = SetHook(proc);
        }
    }

    public void UnblockKeyboard()
    {
        if (hookID != IntPtr.Zero)
        {
            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }
    }

    private static IntPtr SetHook(KeyboardHookProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        return (IntPtr)1;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}