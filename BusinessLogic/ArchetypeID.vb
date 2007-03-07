'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 
Public Class ArchetypeID
    Private mRmEntity As StructureType
    Private mRmModel As ReferenceModelType
    Private mVersion As String
    Private mConcept As String
    Private mArchetypeID As String

    Event ArchetypeID_Changed(ByVal sender As Object, ByVal e As EventArgs)

    ReadOnly Property Reference_Model() As ReferenceModelType
        Get
            Return mRmModel
        End Get
    End Property
    ReadOnly Property VersionAsString() As String
        Get
            Return mVersion
        End Get
    End Property
    ReadOnly Property Version() As Double
        Get
            Return Val(Mid(mVersion, 2))
        End Get
    End Property
    Property Concept() As String
        Get
            Return mConcept
        End Get
        Set(ByVal Value As String)
            mConcept = Value
            mArchetypeID = ReferenceModel.ReferenceModelName & "-" & ReferenceModel.RM_StructureName(mRmEntity) & "." & mConcept & "." & mVersion
            RaiseEvent ArchetypeID_Changed(Me, New EventArgs)
        End Set
    End Property
    ReadOnly Property ReferenceModelEntity() As StructureType
        Get
            Return mRmEntity
        End Get
    End Property

    ReadOnly Property ReferenceModelEntityAsString() As String
        Get
            Return ReferenceModel.RM_StructureName(mRmEntity)
        End Get
    End Property

    Sub SetFromString(ByVal value As String)
        ProcessStringValue(value)
    End Sub

    Overrides Function ToString() As String
        Return mArchetypeID
    End Function

    Private Sub ProcessStringValue(ByVal Value As String)
        Dim y() As String
        mArchetypeID = Value
        'pattern
        'authority-model-entity.concept.version[.detail]
        y = Value.Split(".")
        Debug.Assert(y.Length > 2)
        mConcept = y(1)
        mVersion = y(2)
        y = y(0).Split("-")
        Debug.Assert(y.Length = 3)
        ReferenceModel.SetReferenceModelName(y(0) & "-" & y(1))
        Debug.Assert(ReferenceModel.ModelType <> ReferenceModelType.Not_Set)
        mRmEntity = ReferenceModel.StructureTypeFromString(y(2))
        Debug.Assert(ReferenceModel.IsValidArchetypeDefinition(mRmEntity))
        mRmModel = ReferenceModel.ModelType
    End Sub
    Sub New(ByVal Value As String)
        ProcessStringValue(Value)
    End Sub

End Class

'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is ArchetypeID.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'