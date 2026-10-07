Imports System.Runtime.InteropServices
Imports System.Text

Public Module User32

    <DllImport("user32.dll")>
    Public Function EnumWindows(
        lpEnumFunc As EnumWindowsProc,
        lParam As IntPtr
    ) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Public Function GetWindowText(
        hWnd As IntPtr,
        lpString As StringBuilder,
        nMaxCount As Integer
    ) As Integer
    End Function

    <DllImport("user32.dll")>
    Public Function IsWindowVisible(
        hWnd As IntPtr
    ) As Boolean
    End Function

    Public Delegate Function EnumWindowsProc(
        hWnd As IntPtr,
        lParam As IntPtr
    ) As Boolean

End Module