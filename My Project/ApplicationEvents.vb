' =========================================================================================
' ApplicationEvents.vb
' Handles the WindowsFormsApplicationBase.UnhandledException event. The Inventor COM
' automation flows in Form1/Form7 repeatedly throw transient RPC errors ("The RPC server
' is unavailable" 0x800706BA, "The remote procedure call failed" 0x800706BE) around
' Inventor startup/shutdown that are already handled where practical (WaitForInventorReady,
' RetryInventorCall), but any one that still slips through — e.g. from a background
' finalizer releasing a COM reference after Inventor has already quit — would otherwise
' surface as the framework's own "Unhandled exception" MessageBox and can tear the app down.
' This logs it instead and keeps the application running.
' =========================================================================================
Namespace My

    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Debug.Print("⚠ Unhandled exception suppressed: " & e.Exception.Message)
            e.ExitApplication = False
        End Sub

    End Class

End Namespace
