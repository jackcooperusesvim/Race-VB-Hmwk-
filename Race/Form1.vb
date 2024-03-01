Public Class Form1
    'These next lines Caused a lot of trouble for me. Took me like 30 minutes to realize that the
    ' pointers/references are empty until the form loads, so when I tried to create the arrays here, they would create
    ' these null pointers. Anyway. Just ranting in a comment. I think I hate VB.NET but I'm not so sure now that I (mostly) understand it.
    Public InArr As TextBox(,)
    Public OutArr As TextBox(,)
    Public MaxIndexRaces = 3
    Public MaxIndexBoats = 2
    Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InArr = {{R1B1, R1B2, R1B3}, {R2B1, R2B2, R2B3}, {R3B1, R3B2, R3B3}, {R4B1, R4B2, R4B3}}
        OutArr = {{TotalB1, TotalB2, TotalB3}, {RankB1, RankB2, RankB3}}

    End Sub

    Function Register_Tie()
        Error_Label.Text = "Tie"
        Error_Label.ForeColor = Color.Yellow
    End Function

    Function Throw_Error()
        Error_Label.Text = "Please use only proper integers for Race "
        Error_Label.ForeColor = Color.Red
    End Function
    Function Bad_Numbers()
        Error_Label.Text = "Enter 1, 2 and 3 only for Race "
        Error_Label.ForeColor = Color.Red

    End Function

    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        Dim InArr As TextBox(,) = {{R1B1, R1B2, R1B3}, {R2B1, R2B2, R2B3}, {R3B1, R3B2, R3B3}, {R4B1, R4B2, R4B3}}
        Dim OutArr As TextBox(,) = {{TotalB1, TotalB2, TotalB3}, {RankB1, RankB2, RankB3}}
        MessageBox.Show("starting")
        For RaceNum = 0 To MaxIndexRaces
            For Boat = 0 To MaxIndexBoats
                InArr(RaceNum, Boat).Text = ""
            Next
        Next
        MessageBox.Show("middle")
        For Boat = 0 To MaxIndexBoats
            OutArr(0, Boat).Text = ""
            OutArr(1, Boat).Text = ""
        Next

        Error_Label.Text = ""

    End Sub

    Private Sub Calc_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Btn.Click
        Dim IntArr = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}}
        Dim InArr As TextBox(,) = {{R1B1, R1B2, R1B3}, {R2B1, R2B2, R2B3}, {R3B1, R3B2, R3B3}, {R4B1, R4B2, R4B3}}
        Dim OutArr As TextBox(,) = {{TotalB1, TotalB2, TotalB3}, {RankB1, RankB2, RankB3}}

        Error_Label.Text = ""

        'Check For Bad Input
        For RaceNum = 0 To MaxIndexRaces
            Try
                For Boat = 0 To MaxIndexBoats
                    IntArr(RaceNum, Boat) = Convert.ToInt32(InArr(RaceNum, Boat).Text)
                Next
            Catch ex As Exception
                Throw_Error()
                Error_Label.Text = Error_Label.Text + Convert.ToString(RaceNum + 1)
                Exit For
            End Try
        Next

        'Check for a total of 6
        If Error_Label.Text = "" Then
            Dim Check As Integer
            For RaceNum = 0 To MaxIndexRaces
                Check = 0
                For Boat = 0 To MaxIndexBoats
                    Check = Check + IntArr(RaceNum, Boat)
                Next
                If Not Check = 6 Then
                    Bad_Numbers()
                    Error_Label.Text = Error_Label.Text + Convert.ToString(RaceNum + 1)
                    Exit For
                End If
            Next
        End If

        'Calculate and Update Total
        Dim OutIntArr = {0, 0, 0}
        If Error_Label.Text = "" Then
            OutIntArr = {0, 0, 0}
            For Boat = 0 To MaxIndexBoats
                'Calculate
                OutIntArr(Boat) = 0
                For RaceNum = 0 To MaxIndexRaces
                    OutIntArr(Boat) = OutIntArr(Boat) + IntArr(RaceNum, Boat)
                Next
                'Update
                OutArr(0, Boat).Text = Convert.ToString(OutIntArr(Boat))
            Next
        End If
        'Calculate Order

        'If you were to add more boats the ranking system might 
        'have issues with ties and you would need to update the previous block as well

        'This is kinda like a bubble sort thing
        Dim Ranking = {1, 2, 3}

        Dim uselessvar = 0
        'I don't feel like taking the time to
        'figure out the time complexity of this sort so imma just let it run 5 times. Should be enough???
        For uselessiterator = 0 To 5
            For Boat = 0 To MaxIndexBoats - 1
                'This line is boolean salad, but it's basically taking 2 indices and checking to see if the
                'relative ranking of those two respecive values are obviously wrong (not checking ties) 
                If (OutIntArr(Boat) > OutIntArr(Boat + 1) And Ranking(Boat) > OutIntArr(Boat + 1)) Or (OutIntArr(Boat) < OutIntArr(Boat + 1) And Ranking(Boat) < OutIntArr(Boat + 1)) Then
                    uselessvar = Ranking(Boat)
                    Ranking(Boat) = Ranking(Boat + 1)
                    Ranking(Boat + 1) = uselessvar
                End If

            Next
        Next

        'To handle ties in a way that is easier to compute, I decided to just skip over tied ranks
        'Eg: 4,8,4 becomes 1st(tied),3rd,1st(tied)

        Dim Tied As Boolean
        For Boat = 0 To MaxIndexBoats - 1
            Tied = False
            For BoatTwo = Boat + 1 To MaxIndexBoats
                If OutIntArr(Boat) = OutIntArr(BoatTwo) Then
                    Register_Tie()
                    OutArr(1, Boat).BackColor = Color.Yellow
                    'Couldn't figure out how to do a min/max
                    If Ranking(Boat) > Ranking(BoatTwo) Then
                        Ranking(Boat) = Ranking(BoatTwo)
                    Else
                        Ranking(BoatTwo) = Ranking(Boat)
                    End If
                End If
            Next
            'Display Rankings
            OutArr(1, Boat).Text = Convert.ToString(Ranking(Boat))

        Next
    End Sub


    Private Sub Close_Click(sender As Object, e As EventArgs) Handles Close.Click

    End Sub

End Class
