Option Explicit On

Imports Microsoft.Win32

Public Class FrmMain
    Const sHKCU As String = "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings"
    Const sKeyProxy As String = "ProxyServer"
    Const sKeyHabilitaProxy As String = "ProxyEnable"
    Const sIpPorta As String = "seuip:porta"

    Private Sub SubSetLabels(bStatus As Boolean)
        If bStatus Then
            LblStatus.Text = "ON"
            LblStatus.ForeColor = Color.Green
            BtnEstado.Text = "DESATIVAR"
        Else
            LblStatus.Text = "OFF"
            LblStatus.ForeColor = Color.Red
            BtnEstado.Text = "ATIVAR"
        End If
    End Sub

    Private Sub SubSetProxyRegistro()
        Try
            'Setando ip do proxy
            Registry.SetValue(sHKCU, sKeyProxy, sIpPorta, RegistryValueKind.String)

            'Habilitando o proxy
            Registry.SetValue(sHKCU, sKeyHabilitaProxy, "1", RegistryValueKind.String)

            'Verifica se foi setado realmente
            SubGetProxyRegistro()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub SubDesetProxyRegistro()
        Try
            'Setando ip do proxy como vazio
            Registry.SetValue(sHKCU, sKeyProxy, "", RegistryValueKind.String)

            'Desabilitando o proxy
            Registry.SetValue(sHKCU, sKeyHabilitaProxy, "0", RegistryValueKind.String)

            'Verifica se foi desetado realmente
            SubGetProxyRegistro()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub SubGetProxyRegistro()
        Try
            'Recuperando o estado do proxy
            Dim sValue As String = Registry.GetValue(sHKCU, sKeyHabilitaProxy, "0").ToString

            If sValue = "0" Then
                SubSetLabels(False)
            Else
                SubSetLabels(True)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub BtnEstado_Click(sender As Object, e As EventArgs) Handles BtnEstado.Click
        If BtnEstado.Text = "ATIVAR" Then
            SubSetProxyRegistro()
        Else
            SubDesetProxyRegistro()
        End If
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        SubGetProxyRegistro()
    End Sub
End Class
