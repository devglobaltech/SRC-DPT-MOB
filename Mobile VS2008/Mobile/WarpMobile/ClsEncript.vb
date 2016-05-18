Imports System.Text
Imports System.ComponentModel


Public Class ClsEncript
    Public Shared Function EncryptString(ByVal InputString As String, ByVal SecretKey As String, Optional ByVal CyphMode As Security.Cryptography.CipherMode = Security.Cryptography.CipherMode.ECB) As String
        Try
            Dim Des As New Security.Cryptography.TripleDESCryptoServiceProvider
            'Put the string into a byte array
            Dim InputbyteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(InputString)

            'Create the crypto objects, with the key, as passed in
            Dim hashMD5 As New Security.Cryptography.MD5CryptoServiceProvider
            Des.Key = hashMD5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(SecretKey))
            Des.Mode = CyphMode
            Dim ms As New IO.MemoryStream
            Dim cs As Security.Cryptography.CryptoStream = New Security.Cryptography.CryptoStream(ms, Des.CreateEncryptor(), Security.Cryptography.CryptoStreamMode.Write)

            'Write the byte array into the crypto stream
            '(It will end up in the memory stream)
            cs.Write(InputbyteArray, 0, InputbyteArray.Length)
            cs.FlushFinalBlock()

            'Get the data back from the memory stream, and into a string
            Dim ret As System.Text.StringBuilder = New System.Text.StringBuilder
            Dim b() As Byte = ms.ToArray
            ms.Close()
            Dim I As Integer
            For I = 0 To UBound(b)
                'Format as hex
                ret.AppendFormat("{0:X2}", b(I))
            Next

            Return ret.ToString()
        Catch ex As System.Security.Cryptography.CryptographicException
            MsgBox("Fallo al Encryptar. " & ex.Message, MsgBoxStyle.OkOnly)
            Return ""
        End Try

    End Function

    Public Shared Function DecryptString(ByVal InputString As String, ByVal SecretKey As String, Optional ByVal CyphMode As Security.Cryptography.CipherMode = Security.Cryptography.CipherMode.ECB) As String
        If InputString = String.Empty Then
            Return ""
        Else
            Dim Des As New Security.Cryptography.TripleDESCryptoServiceProvider
            'Put the string into a byte array
            Dim InputbyteArray(CType(InputString.Length / 2 - 1, Integer)) As Byte '= Encoding.UTF8.GetBytes(InputString)
            'Create the crypto objects, with the key, as passed in
            Dim hashMD5 As New Security.Cryptography.MD5CryptoServiceProvider

            Des.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(SecretKey))
            Des.Mode = CyphMode
            'Put the input string into the byte array

            Dim X As Integer

            For X = 0 To InputbyteArray.Length - 1
                Dim IJ As Int32 = (Convert.ToInt32(InputString.Substring(X * 2, 2), 16))
                Dim BT As New System.ComponentModel.TypeConverter 'System.ComponentModel.ByteConverter
                InputbyteArray(X) = New Byte
                InputbyteArray(X) = CType(BT.ConvertTo(IJ, GetType(Byte)), Byte)
            Next

            Dim ms As IO.MemoryStream = New IO.MemoryStream
            Dim cs As Security.Cryptography.CryptoStream = New Security.Cryptography.CryptoStream(ms, Des.CreateDecryptor(), _
            Security.Cryptography.CryptoStreamMode.Write)

            'Flush the data through the crypto stream into the memory stream
            cs.Write(InputbyteArray, 0, InputbyteArray.Length)
            cs.FlushFinalBlock()

            '//Get the decrypted data back from the memory stream
            Dim ret As System.Text.StringBuilder = New System.Text.StringBuilder
            Dim B() As Byte = ms.ToArray

            ms.Close()

            Dim I As Integer

            For I = 0 To UBound(B)
                ret.Append(Chr(B(I)))
            Next

            Return ret.ToString()
        End If
    End Function

End Class

