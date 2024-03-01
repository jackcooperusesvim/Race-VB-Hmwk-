Public Class Form1
    Function Throw_Error()
        Error_Label.Text = "Please use only proper integers for Race "
    End Function
    Function Bad_Numbers()
        Error_Label.Text = "Enter 1, 2 and 3 only for Race "
    End Function

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
        Dim InArr as TextBox = {{R1B1,R1B2,R1B3},{R2B1,R2B2,R2B3},{R3B1,R3B2,R3B3}}
        Dim OutArr as TextBox = {{TotalB1,TotalB2,TotalB3},{RankB1,RankB2,RankB3}}

        Dim IntArr as TextBox

        Error_Label.Text = ""

        'Check For Bad Input
        For Race = 0 To 2
            Try:
                For Boat = 0 To 2
                    IntArr(Race,Boat) = Convert.ToInt32(InArr(Race,Boat).Text)
                Next
            Catch ex As Exception
                Bad_Numbers()
                Error_Label.Text = Error_Label.Text+Convert.ToString(Race+1)
                Exit For
            End Try
            Next
        Next

        'Check for a total of 6
        If Error_Label.Text == ""
            Dim Check As Integer 
            For Race = 0 To 2
                Check = 0
                For Boat = 0 To 2
                    Check = Check + IntArr(Race,Boat)
                Next
                If Check != 6
                    Bad_Numbers()
                    Error_Label.Text = Error_Label.Text+Convert.ToString(Race+1)
                    Exit For
                End If
            Next
        End If

        'Calculate and Update Total
        If Error_Label.Text == ""
            OutIntArr As Integer = {0,0,0}
            For Boat = 0 To 2
                'Calculate
                OutIntArr(Boat) = 0
                For Race = 0 To 2
                    OutIntArr(Boat) = OutIntArr(Boat) + IntArr(Boat,Race)
                Next
                'Update
                OutArr(0,Boat).Text = Convert.ToString(OutIntArr)
            Next
       End If
    End Sub


    Private Sub Close_Click(sender As Object, e As EventArgs) Handles Close.Click

    End Sub
End Class
