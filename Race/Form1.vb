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
        Error_Label.Text = ""
    End Sub

    Function Register_Tie()
        Error_Label.Text = "Tie"
        Error_Label.ForeColor = Color.Orange
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

        For RaceNum = 0 To MaxIndexRaces
            For Boat = 0 To MaxIndexBoats
                InArr(RaceNum, Boat).Text = ""
            Next
        Next
        For Boat = 0 To MaxIndexBoats
            OutArr(0, Boat).Text = ""
            OutArr(1, Boat).Text = ""
            OutArr(1, Boat).BackColor = DefaultBackColor

        Next

        Error_Label.Text = ""

    End Sub

    Private Sub Calc_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Btn.Click
        Dim IntArr = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}}

        For Boat = 0 To MaxIndexBoats
            OutArr(1, Boat).BackColor = DefaultBackColor
        Next
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

            'Calculate Order

            'If you were to add more boats the ranking system might 
            'have issues with ties and you would need to update the previous block as well

            'This is kinda like a bubble sort thing
            Dim Ranking = {1, 2, 3}

            Dim uselessvar = 0
            'I don't feel like taking the time to
            'figure out the time complexity of this sort so imma guess that 3 reps should be enough??? N?
            For uselessiterator = 0 To 2
                For Boat = 0 To MaxIndexBoats - 1
                    For BoatTwo = Boat To MaxIndexBoats
                        'This line is boolean salad, but it's basically taking 2 indices and checking to see if the
                        'relative ranking of those two respecive values are obviously wrong (not checking ties) 
                        If (OutIntArr(Boat) > OutIntArr(BoatTwo) And Ranking(Boat) < OutIntArr(BoatTwo)) Or (OutIntArr(Boat) < OutIntArr(BoatTwo) And Ranking(Boat) > OutIntArr(BoatTwo)) Then
                            uselessvar = Ranking(Boat)
                            Ranking(Boat) = Ranking(BoatTwo)
                            Ranking(BoatTwo) = uselessvar
                        End If
                    Next
                Next
            Next


            'To handle ties in a way that is easier to compute, I decided to just skip over tied ranks
            'Eg: 4,8,4 becomes 1st(tied),3rd,1st(tied)

            Dim Ties As Boolean() = {False, False, False}
            For Boat = 0 To MaxIndexBoats - 1
                For BoatTwo = Boat + 1 To MaxIndexBoats
                    If OutIntArr(Boat) = OutIntArr(BoatTwo) Then
                        Register_Tie()
                        Ties(Boat) = True
                        Ties(BoatTwo) = True
                        'Couldn't figure out how to do a min/max
                        If Ranking(Boat) > Ranking(BoatTwo) Then
                            Ranking(Boat) = Ranking(BoatTwo)
                        Else
                            Ranking(BoatTwo) = Ranking(Boat)
                        End If
                    End If
                Next

            Next
            'Display Rankings
            For Boat = 0 To MaxIndexBoats
                'Display Rankings and Ties
                If Ties(Boat) Then
                    OutArr(1, Boat).BackColor = Color.Orange
                End If
                OutArr(1, Boat).Text = Convert.ToString(Ranking(Boat))

            Next
        End If

    End Sub


    Private Sub Close_Click(sender As Object, e As EventArgs) Handles Close.Click
        'Got this lil bit from stackoverflow
        Application.Exit()
        End
    End Sub

End Class
