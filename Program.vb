Imports System

Module Program
    <Obsolete>
    Sub Main(args As String())

        '## Gas Po Main 
        Dim GasPOMain As New List(Of GasPOMain)
        GasPOMain.Add(New GasPOMain With {.Receipt_ID = 1, .Receipt_Number = 100})
        GasPOMain.Add(New GasPOMain With {.Receipt_ID = 2, .Receipt_Number = 200})


        '## Gas PO Receipt
        Dim ReceiptList As New List(Of Receipt)
        ReceiptList.Add(New Receipt With {.Receipt_Number = 100, .Items = "Diesel"})
        ReceiptList.Add(New Receipt With {.Receipt_Number = 100, .Items = "Others"})
        ReceiptList.Add(New Receipt With {.Receipt_Number = 200, .Items = "Others"})
        ReceiptList.Add(New Receipt With {.Receipt_Number = 200, .Items = "Unleaded"})

        Dim que_JoinList = (From main In GasPOMain.AsEnumerable()
                            Join rec In ReceiptList.AsEnumerable() On main.Receipt_Number Equals rec.Receipt_Number
                            Select main, rec).ToList()

        Dim dtList As New List(Of dtList)


        Dim res = que_JoinList

        For Each x In res
            dtList.Add(New dtList With {.Receipt_Number = x.main.Receipt_Number, .Items = x.rec.Items})
        Next

        Dim groupedAndTransformed = From d In dtList
                                    Group d By d.Receipt_Number Into g = Group
                                    Select New With {
                                            Key .Untransformed = g,
                                            Key .Transformed = Receipt_Number & " " & String.Join(",", g.Select(Function(tuple) tuple.Items))
                                        }

        Console.WriteLine($"With Grouping")
        For Each x In groupedAndTransformed
            Console.WriteLine($"{x.Transformed}")

        Next

        Console.WriteLine()
        Console.WriteLine($"Without Grouping")
        For Each x In que_JoinList
            Console.WriteLine($"{x.rec.Items}, {x.main.Receipt_Number}")
        Next

    End Sub

    Class dtList
        Public Property Receipt_Number As Integer
        Public Property Items As String
    End Class

    Class Receipt
        Public Property Receipt_Number As Integer
        Public Property Items As String
    End Class

    Class GasPOMain
        Public Property Receipt_ID As Integer
        Public Property Receipt_Number As Integer
    End Class
End Module
