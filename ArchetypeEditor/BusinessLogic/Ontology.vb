'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public MustInherit Class Ontology
    Protected keys(1) As Object
    Protected iSpecialisations As Integer
    Protected a_Term As RmTerm

    Public MustOverride ReadOnly Property PrimaryLanguageCode() As String
    Public MustOverride ReadOnly Property LanguageCode() As String
    Public MustOverride ReadOnly Property NumberOfSpecialisations() As Integer
    Public MustOverride Sub Reset()
    Public MustOverride Function LanguageAvailable(ByVal code As String) As Boolean
    Public MustOverride Function IsMultiLanguage() As Boolean
    Public MustOverride Function TerminologyAvailable(ByVal code As String) As Boolean
    Public MustOverride Sub SetLanguage(ByVal code As String)
    Public MustOverride Sub SetPrimaryLanguage(ByVal LanguageCode As String)
    Public MustOverride Function SpecialiseTerm(ByVal Text As String, ByVal Description As String, ByVal Id As String) As RmTerm
    Public MustOverride Function NextTermId() As String
    Public MustOverride Function NextConstraintID() As String
    Public MustOverride Sub AddTerm(ByVal a_Term As RmTerm)
    Public MustOverride Sub ReplaceTerm(ByVal a_Term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)
    Public MustOverride Sub AddConstraint(ByVal a_Term As RmTerm)
    Public MustOverride Sub ReplaceConstraint(ByVal a_Term As RmTerm, Optional ByVal ReplaceTranslations As Boolean = False)
    Public MustOverride Sub AddLanguage(ByVal LanguageCode As String)
    Public MustOverride Sub AddTerminology(ByVal TerminologyCode As String)
    Public MustOverride Sub AddorReplaceTermBinding(ByVal sTerminology As String, ByVal sPath As String, ByVal sCode As String, ByVal sRelease As String)
    Public MustOverride Function TermForCode(ByVal Code As String, ByVal LanguageCode As String) As RmTerm
    Public MustOverride Sub PopulateAllTerms(ByRef TheOntologyManager As OntologyManager)
    Public MustOverride Sub PopulateTermsInLanguage(ByRef TheOntologyManager As OntologyManager, ByVal LanguageCode As String)

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
'The Original Code is Ontology.vb.
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