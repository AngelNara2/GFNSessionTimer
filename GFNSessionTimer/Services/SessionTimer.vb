Public Class SessionTimer

    Private ReadOnly _sessionDuration As TimeSpan = TimeSpan.FromMinutes(60)

    Private _startTime As DateTime
    Private _isRunning As Boolean

    Public ReadOnly Property IsRunning As Boolean
        Get
            Return _isRunning
        End Get
    End Property

    Public ReadOnly Property RemainingTime As TimeSpan
        Get

            If Not _isRunning Then
                Return _sessionDuration
            End If

            Dim elapsed As TimeSpan = DateTime.Now - _startTime
            Dim remaining As TimeSpan = _sessionDuration - elapsed

            If remaining <= TimeSpan.Zero Then
                Return TimeSpan.Zero
            End If

            Return remaining

        End Get
    End Property

    Public ReadOnly Property RemainingTimeText As String
        Get

            Dim remaining As TimeSpan = RemainingTime

            Return $"{CInt(remaining.TotalMinutes):00}:{remaining.Seconds:00}"

        End Get
    End Property

    Public Sub Start()

        _startTime = DateTime.Now
        _isRunning = True

    End Sub

    Public Sub Pause()

        _isRunning = False

    End Sub

    Public Sub Reset()

        _startTime = DateTime.MinValue
        _isRunning = False

    End Sub

End Class