Public Class Form1
    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        R1B1.Text = ""
        R1B2.Text = ""
        R1B3.Text = ""

        R2B1.Text = ""
        R2B2.Text = ""
        R2B3.Text = ""

        R3B1.Text = ""
        R3B2.Text = ""
        R3B3.Text = ""
    End Sub

    Private Sub Calc_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Btn.Click
        Try
            Convert.ToInt32(R1B1)+Convert.ToInt32(R1B1)+Convert.ToInt32(R1B1)+
        Catch ex As Exception

        End Try
    End Sub
End Class
