Imports System

Module Program
    <Obsolete>
    Sub Main(args As String())

        '## Gas Po Main 
        Dim GasPOMain As New List(Of GasPOMain)
        GasPOMain.Add(New GasPOMain With {.Receipt_ID = 1, .Receipt_Number = 100})
        GasPOMain.Add(New GasPOMain With {.Receipt_ID = 2, .Receipt_Number = 200})
        GasPOMain.Add(New GasPOMain With {.Receipt_ID = 2, .Receipt_Number = 300})


        '## Gas PO Receipt
        Dim ReceiptList As New List(Of Receipt)
        ReceiptList.Add(New Receipt With {.Receipt_Number = 100, .Items = "Diesel"})
        ReceiptList.Add(New Receipt With {.Receipt_Number = 100, .Items = "Others"})
        ReceiptList.Add(New Receipt With {.Receipt_Number = 200, .Items = "Others"})
        ReceiptList.Add(New Receipt With {.Receipt_Number = 200, .Items = "Unleaded"})
        ReceiptList.Add(New Receipt With {.Receipt_Number = 300, .Items = "test"})

        Dim que_JoinList = (From main In GasPOMain.AsEnumerable()
                            Join rec In ReceiptList.AsEnumerable() On main.Receipt_Number Equals rec.Receipt_Number
                            Select main, rec).ToList()

        Dim dtList As New List(Of dtList)
        Dim finalList As New List(Of finalList)


        Dim res = que_JoinList

        For Each x In res
            dtList.Add(New dtList With {.Receipt_Number = x.main.Receipt_Number, .Items = x.rec.Items})

        Next

        Dim groupedAndTransformed = From d In dtList
                                    Group d By d.Receipt_Number Into g = Group
                                    Select New With {
                                            Key .Untransformed = g,
                                            Key .ReceiptNumber = Receipt_Number,
                                            Key .Transformed = String.Join(",", g.Select(Function(tuple) tuple.Items))
                                        }
        For Each xx In groupedAndTransformed
            finalList.Add(New finalList With {.Receipt_Number = xx.ReceiptNumber, .Items = xx.Transformed})
        Next




        Console.WriteLine($"With Grouping")
        For Each x In finalList
            Console.WriteLine($"{x.Receipt_Number}, {x.Items}")

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

    Class finalList
        Public Property Items As String
        Public Property Receipt_Number As Integer
    End Class
End Module
