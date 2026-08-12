Public Module LegSupportState

    Public Enum SupportType
        Angle
        Beam
    End Enum

    Private _currentType As SupportType = SupportType.Angle

    Public Event SupportTypeChanged(newType As SupportType)

    Public Property CurrentSupportType As SupportType
        Get
            Return _currentType
        End Get
        Set(value As SupportType)
            If _currentType <> value Then
                _currentType = value
                RaiseEvent SupportTypeChanged(value)
            End If
        End Set
    End Property

End Module