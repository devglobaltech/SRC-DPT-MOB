Option Explicit On

Imports System.Data
Imports System.Data.SqlClient

Public Class clsEmpaquetado

    Private o2D As clsDecode2D
    Private Conn As SqlConnection
    Private Const ClsName As String = "Empaquetado."
    Private vCliente_ID As String = ""

    Public Sub New()
        o2d = New clsDecode2D
        Conn = New SqlConnection
    End Sub

    Protected Overrides Sub Finalize()
        o2D = Nothing
        Conn = Nothing
        MyBase.Finalize()
    End Sub

    Public Property Conexion() As SqlConnection
        Get
            Return Conn
        End Get
        Set(ByVal value As SqlConnection)
            Conn = value
        End Set
    End Property



End Class
