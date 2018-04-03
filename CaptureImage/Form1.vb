
Imports Emgu.CV
Imports Emgu.CV.UI
Imports Emgu.CV.Structure

Public Class Form1

    '    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
    '        Cursor.Clip = PictureBox1.Cursor.Clip
    '        Cursor.Current = Cursors.Cross
    '    Dim pt As Point 'added by AI
    '
    '
    '    If mea.Button = MouseButtons.Left Then
    '            pt = New Point(mea.X, mea.Y)
    '
    '            lX.Text = Str(pt.X)
    '            lY.Text = Str(pt.Y)
    '    End If
    '    End Sub





    Dim cap As New Capture() 'first line
  
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PictureBox1.Image = cap.QueryFrame.ToBitmap() 'Second line
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Timer1.Start()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PictureBox1.Image = Nothing
        'PictureBox1.cls

    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        Dim mycolor As Color
        mycolor = scGetPixel(PictureBox1.Image, e.X, e.Y)
        lX.Text = Str(e.X)
        lY.Text = Str(e.Y)

        Panel1.BackColor = mycolor
        txtValueColor.Text = mycolor.ToString
        Panel15.BackColor = mQuantize(mycolor)
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        'Dim mycolor As Color
        'mycolor = mGetPixel(PictureBox1.Image, e.X, e.Y)
        'Panel1.BackColor = mycolor
        'txtValueColor.Text = mycolor.ToString
        'Panel15.BackColor = mQuantize(mycolor)
    End Sub

    Private Function scGetPixel(mpb As Image, x As Integer, y As Integer) As Color
        Dim ScaleFactorX As Single
        Dim ScaleFactorY As Single

        Dim MyImage As Bitmap = CType(PictureBox1.Image, Bitmap)
        ScaleFactorX = PictureBox1.Width / MyImage.Width
        ScaleFactorY = PictureBox1.Height / MyImage.Height

        'Me.BackColor = MyImage.GetPixel(CInt(e.X / ScaleFactorX), CInt(e.Y / ScaleFactorY))
        Return MyImage.GetPixel(CInt(x / ScaleFactorX), CInt(y / ScaleFactorY))
    End Function

    Private Function mQuantize(icol As Color) As Color
        Dim qcol As Color

        Dim r As Integer
        Dim g As Integer
        Dim b As Integer

        r = icol.R
        g = icol.G
        b = icol.B

        If r < 25 And b < 25 And g < 25 Then qcol = Color.Black
        If r > 95 And r < 188 And g > 50 And g < 100 And b > 9 And b < 63 Then qcol = Color.Orange
        Return qcol

    End Function

    Private Sub SetPixel_Example(ByVal e As PaintEventArgs)

        ' Create a Bitmap object from a file.
        'Dim myBitmap As New Bitmap("Grapes.jpg")
        Dim myBitmap As New Bitmap(PictureBox1.Image)
        ' Draw myBitmap to the screen.
        e.Graphics.DrawImage(myBitmap, 0, 0, myBitmap.Width,
        myBitmap.Height)

        ' Set each pixel in myBitmap to black.
        Dim Xcount As Integer
        For Xcount = 0 To myBitmap.Width - 1
            Dim Ycount As Integer
            For Ycount = 0 To myBitmap.Height - 1
                myBitmap.SetPixel(Xcount, Ycount, Color.Black)
            Next Ycount
        Next Xcount

        ' Draw myBitmap to the screen again.
        e.Graphics.DrawImage(myBitmap, myBitmap.Width, 0, myBitmap.Width,
            myBitmap.Height)
    End Sub

    Private Function getMouseLoc() As Point
        getMouseLoc = Me.PointToClient(Me.Cursor.Position)
        Return getMouseLoc
    End Function

    Protected Function TranslateStretchImageMousePosition(ByVal coordinates As Point) As Point
        ' test to make sure our image is not null
        If (PictureBox1.Image Is Nothing) Then
            Return coordinates
        End If

        ' Make sure our control width and height are not 0
        If ((Width = 0) _
                    OrElse (Height = 0)) Then
            Return coordinates
        End If

        ' First, get the ratio (image to control) the height and width
        Dim ratioWidth As Single = (CType(PictureBox1.Image.Width, Single) / Width)
        Dim ratioHeight As Single = (CType(PictureBox1.Image.Height, Single) / Height)
        ' Scale the points by our ratio
        Dim newX As Single = coordinates.X
        Dim newY As Single = coordinates.Y
        newX = (newX * ratioWidth)
        newY = (newY * ratioHeight)
        Return New Point(CType(newX, Integer), CType(newY, Integer))
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Button3_Click(sender, e)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'MsgBox(ComboBox1.Text)
        'Dim
        'MsgBox(ComboBox1.SelectedIndex)
        DrawBands(ComboBox1.SelectedIndex)
    End Sub

    Private Sub DrawBands(bands As Integer)
        'Dim g As Graphics = PictureBox1.CreateGraphics
        'g.DrawLine(Pens.Black, 100, 0, 100, 100)
        ''Dim g As Graphics = System.Drawing.Graphics
        'Dim myPen As New System.Drawing.Pen(System.Drawing.Color.Red)
        'Dim formGraphics As System.Drawing.Graphics
        'formGraphics = Me.CreateGraphics()
        'formGraphics.DrawLine(myPen, 0, 0, 200, 200)
        'myPen.Dispose()
        'formGraphics.Dispose()
        Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Red)
        Dim clsBrush As New System.Drawing.SolidBrush(System.Drawing.Color.White)
        Dim formGraphics As System.Drawing.Graphics
        formGraphics = Me.CreateGraphics()

        formGraphics.FillRectangle(clsBrush, New Rectangle(12, 0, 330, 40))
        formGraphics.FillRectangle(clsBrush, New Rectangle(12, 270, 330, 40))

        If bands = 0 Then     'bands is actually = 3
            formGraphics.FillRectangle(myBrush, New Rectangle(52, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(150, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(248, 275, 40, 20))
            formGraphics.DrawLine(Pens.Black, 72, 0, 72, 300)
            formGraphics.DrawLine(Pens.Black, 170, 0, 170, 300)
            formGraphics.DrawLine(Pens.Black, 268, 0, 268, 300)
        End If
        If bands = 1 Then     'bands is actually = 4
            formGraphics.FillRectangle(myBrush, New Rectangle(37, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(114, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(191, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(268, 275, 40, 20))
            formGraphics.DrawLine(Pens.Black, 57, 0, 57, 300)
            formGraphics.DrawLine(Pens.Black, 134, 0, 134, 300)
            formGraphics.DrawLine(Pens.Black, 211, 0, 211, 300)
            formGraphics.DrawLine(Pens.Black, 288, 0, 288, 300)
        End If
        If bands = 2 Then     'bands is actually = 5
            formGraphics.FillRectangle(myBrush, New Rectangle(27, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(89, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(151, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(213, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(275, 275, 40, 20))
            formGraphics.DrawLine(Pens.Black, 47, 0, 47, 300)
            formGraphics.DrawLine(Pens.Black, 109, 0, 109, 300)
            formGraphics.DrawLine(Pens.Black, 171, 0, 171, 300)
            formGraphics.DrawLine(Pens.Black, 233, 0, 233, 300)
            formGraphics.DrawLine(Pens.Black, 295, 0, 295, 300)
        End If
        If bands = 3 Then    'bands is actually = 6
            formGraphics.FillRectangle(myBrush, New Rectangle(22, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(74, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(126, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(178, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(230, 275, 40, 20))
            formGraphics.FillRectangle(myBrush, New Rectangle(282, 275, 40, 20))
            formGraphics.DrawLine(Pens.Black, 42, 0, 42, 300)
            formGraphics.DrawLine(Pens.Black, 94, 0, 94, 300)
            formGraphics.DrawLine(Pens.Black, 146, 0, 146, 300)
            formGraphics.DrawLine(Pens.Black, 198, 0, 198, 300)
            formGraphics.DrawLine(Pens.Black, 250, 0, 250, 300)
            formGraphics.DrawLine(Pens.Black, 302, 0, 302, 300)
        End If

        myBrush.Dispose()
        formGraphics.Dispose()

    End Sub

    Sub makelines()
        Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Red)
        Dim clsBrush As New System.Drawing.SolidBrush(System.Drawing.Color.White)
        Dim formGraphics As System.Drawing.Graphics
        Dim pbGraphics As System.Drawing.Graphics
        formGraphics = Me.CreateGraphics()
        pbGraphics = Me.PictureBox1.CreateGraphics()

        pbGraphics.DrawLine(Pens.Black, 35, 0, 100, 100)
        pbGraphics.DrawLine(Pens.Black, 97, 0, 126, 100)
        pbGraphics.DrawLine(Pens.Black, 159, 0, 149, 100)
        pbGraphics.DrawLine(Pens.Black, 221, 0, 166, 100)
        pbGraphics.DrawLine(Pens.Black, 282, 0, 198, 100)

        pbGraphics.DrawLine(Pens.Black, 100, 151, 35, 254)
        pbGraphics.DrawLine(Pens.Black, 126, 143, 97, 254)
        pbGraphics.DrawLine(Pens.Black, 149, 143, 159, 254)
        pbGraphics.DrawLine(Pens.Black, 166, 143, 221, 254)
        pbGraphics.DrawLine(Pens.Black, 198, 151, 282, 254)

    End Sub


    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Button3_Click(sender, e)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        makelines()

    End Sub
End Class
