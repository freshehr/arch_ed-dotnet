Public Class ArchetypeSlot : Inherits ArchetypeNodeAbstract

    Private Property Slot() As RmSlot
        Get
            Return CType(Item, RmSlot)
        End Get
        Set(ByVal Value As RmSlot)
            Item = Value
        End Set
    End Property

    Public Shadows ReadOnly Property RM_Class() As RmSlot
        Get
            Debug.Assert(TypeOf MyBase.RM_Class Is RmSlot)
            Return CType(MyBase.RM_Class, RmSlot)
        End Get
    End Property

    Public Property Constraint() As Constraint
        Get
            Return Slot.SlotConstraint
        End Get
        Set(ByVal Value As Constraint)
            Slot.SlotConstraint = Value
        End Set
    End Property

    Public Overrides Function Copy() As ArchetypeNode
        Return New ArchetypeSlot(CType(Slot.Copy, RmSlot), mFileManager)
    End Function

    Public Overrides Property Text() As String
        Get
            Dim s As String = mFileManager.OntologyManager.GetOpenEHRTerm(Slot.SlotConstraint.RM_ClassType, Slot.SlotConstraint.RM_ClassType.ToString)
            Return String.Format("{0} [{1}]", MyBase.Text, s)
        End Get
        Set(ByVal value As String)
            Dim i As Integer = value.IndexOf("["c)
            If i > 0 Then
                '// IMCN 02 July 2010 EDT-609: strip spurious space character
                If (i > 1) And value.Substring(i - 1, 1) = " " Then
                    i = i - 1
                End If
                MyBase.Text = value.Substring(0, i)
            Else
                MyBase.Text = value
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property HasReferences() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property IsReference() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String
        Dim s As New System.Text.StringBuilder()
        Dim slot_constraint As Constraint_Slot

        Try
            slot_constraint = Slot.SlotConstraint
        Catch ex As Exception
            Return ""
        End Try

        s.Append("<tr><td><table><tr><td width=""")
        s.Append((level * 20).ToString)
        s.Append("""></td><td>")


        s.Append("<img border=""0"" src=""Images/slot.gif"" width=""32"" height=""32"" align=""middle"">")
        s.AppendLine("</td></tr></table></td>")

        's &= Environment.NewLine & "<tr>"
        s.AppendFormat("<td>{0}<br>{1}</td>", Filemanager.GetOpenEhrTerm(312, "Slot"), Me.Text)
        s.AppendLine()

        Dim include_label As String
        Dim exclude_label As String

        If slot_constraint.RM_ClassType = StructureType.SECTION Then
            include_label = Filemanager.GetOpenEhrTerm(172, "Include sections")
            exclude_label = Filemanager.GetOpenEhrTerm(173, "Exclude sections")
        ElseIf slot_constraint.RM_ClassType = StructureType.ENTRY Then
            include_label = Filemanager.GetOpenEhrTerm(175, "Include entries")
            exclude_label = Filemanager.GetOpenEhrTerm(176, "Exclude entries")
        Else
            include_label = Filemanager.GetOpenEhrTerm(625, "Include") & " : " & slot_constraint.RM_ClassType.ToString
            exclude_label = Filemanager.GetOpenEhrTerm(626, "Exclude") & " : " & slot_constraint.RM_ClassType.ToString
        End If

        include_label &= "<br>"
        exclude_label &= "<br>"

        s.AppendFormat("<td>{0}", include_label)
        If slot_constraint.Include.Count > 0 Then
            If slot_constraint.IncludeAll Then
                s.Append(Filemanager.GetOpenEhrTerm(11, "Allow all"))
            Else
                For Each statement As String In slot_constraint.Include
                    s.AppendFormat("{0}{1}<br>", Environment.NewLine, statement)
                Next

            End If
        End If
        s.AppendLine("</td>")

        s.AppendFormat("<td>{0}", exclude_label)
        If slot_constraint.Exclude.Count > 0 Then
            If slot_constraint.ExcludeAll Then
                s.Append(Filemanager.GetOpenEhrTerm(11, "Allow all"))
            Else
                For Each statement As String In slot_constraint.Exclude
                    s.AppendFormat("{0}{1}<br>", Environment.NewLine, statement)
                Next

            End If
        End If
        s.Append("</td>")
        If OceanArchetypeEditor.Instance.Options.ShowCommentsInHtml Then
            s.Append("<td>&nbsp;</td>")
        End If
        s.Append("</tr>")
        Return s.ToString()

    End Function

    Public Overrides Function ToRichText(ByVal level As Integer) As String
        Dim s, statement As String
        Dim slot_constraint As Constraint_Slot

        Try
            slot_constraint = Slot.SlotConstraint
        Catch ex As Exception
            Return ""
        End Try

        s = Space(3 * level) & slot_constraint.RM_ClassType.ToString & ":\par"
        If slot_constraint.IncludeAll Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Include ALL\par"
        ElseIf slot_constraint.Include.Count > 0 Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Include:\par"
            For Each statement In slot_constraint.Include
                s &= Environment.NewLine & Space(3 * (level + 2)) & statement & "\par"
            Next
        End If


        If slot_constraint.ExcludeAll Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Exclude ALL\par"
        ElseIf slot_constraint.Exclude.Count > 0 Then
            s &= Environment.NewLine & Space(3 * (level + 1)) & "  Exclude:\par"
            For Each statement In slot_constraint.Exclude
                s &= Environment.NewLine & Space(3 * (level + 2)) & statement & "\par"
            Next
        End If
        Return s
    End Function

    Sub New(ByVal a_slot As RmSlot, ByVal aFileManager As FileManagerLocal)
        MyBase.New(a_slot, aFileManager)
    End Sub

    Sub New(ByVal UnnamedSlot As ArchetypeNodeAnonymous, ByVal aFileManager As FileManagerLocal)
        MyBase.New(UnnamedSlot.RM_Class, aFileManager)
        Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm(ReferenceModel.RM_StructureName(Slot.SlotConstraint.RM_ClassType))
        mText = aTerm.Text
        Slot.NodeId = aTerm.Code
        mDescription = ""
    End Sub

    Sub New(ByVal a_text As String, ByVal slotClass As StructureType, ByVal aFileManager As FileManagerLocal)
        MyBase.New(a_text)
        mFileManager = aFileManager
        Dim aTerm As RmTerm = mFileManager.OntologyManager.AddTerm(a_text)
        mDescription = aTerm.Description
        mComment = aTerm.Comment
        Slot = New RmSlot(slotClass)
        Slot.NodeId = aTerm.Code
    End Sub

End Class
