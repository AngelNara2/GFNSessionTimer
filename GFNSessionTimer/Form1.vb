Imports System.Runtime.InteropServices
Imports System.Text

Public Class Form1

    Private WithEvents DetectorTimer As New Timer With {
        .Interval = 1
    }

    Private WithEvents Timer As New Timer With {
        .Interval = 1000
    }

    Private _geforceWindowHandle As IntPtr = IntPtr.Zero

    Private _lastWindowTitle As String = String.Empty

    Private ReadOnly _sessionTimer As New SessionTimer()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = "GFNSessionTimer"

        DetectorTimer.Start()

        lblEstado.Text = "Buscando GeForce NOW..."
        lblTitulo.Text = ""
        lblTimer.Text = ""
    End Sub

    Private Sub DetectorTimer_Tick(sender As Object, e As EventArgs) Handles DetectorTimer.Tick
        DetectarGeForceNow()
    End Sub
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        If Not _sessionTimer.IsRunning Then lblTimer.Text = "60:00" : Exit Sub

        lblTimer.Text = _sessionTimer.RemainingTimeText
    End Sub

    Private Sub DetectarGeForceNow()
        Dim ventanaEncontrada As IntPtr = IntPtr.Zero
        Dim tituloEncontrado As String = String.Empty

        EnumWindows(
            Function(hWnd, lParam)

                If Not IsWindowVisible(hWnd) Then
                    Return True
                End If

                Dim titulo As New StringBuilder(512)

                GetWindowText(
                    hWnd,
                    titulo,
                    titulo.Capacity
                )

                Dim texto As String = titulo.ToString()

                If texto.IndexOf(
                    "GeForce NOW",
                    StringComparison.OrdinalIgnoreCase
                ) >= 0 Then

                    ventanaEncontrada = hWnd
                    tituloEncontrado = texto

                    Return False
                End If

                Return True

            End Function,
            IntPtr.Zero
        )

        If ventanaEncontrada <> IntPtr.Zero Then

            _geforceWindowHandle = ventanaEncontrada

            lblEstado.Text = "GeForce NOW detectado"
            lblTitulo.Text = tituloEncontrado

            If tituloEncontrado = "GeForce NOW" Then _
                If _sessionTimer.IsRunning Then _sessionTimer.Reset() : Exit Sub


            If tituloEncontrado <> _lastWindowTitle Then

                _lastWindowTitle = tituloEncontrado

                Threading.Thread.Sleep(20000)

                _sessionTimer.Start()
                Timer.Start()
            End If

        Else

            _geforceWindowHandle = IntPtr.Zero

            lblEstado.Text = "GeForce NOW no detectado"

            _lastWindowTitle = String.Empty

        End If
    End Sub
End Class