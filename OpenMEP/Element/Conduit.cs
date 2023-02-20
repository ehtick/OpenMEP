﻿using Autodesk.Revit.DB;
using Dynamo.Graph.Nodes;
using OpenMEP.Helpers;
using RevitServices.Persistence;
using RevitServices.Transactions;

namespace OpenMEP.Element;

public class Conduit
{
    private Conduit()
    {
        
    }

    /// <summary>Creates a new instance of conduit.</summary>
    /// <remarks>This method will regenerate the document.</remarks>
    /// <param name="conduitType">
    ///    The conduit type.  This must be a conduit type accepted by isValidConduitType().
    ///    If the input conduit type is InvalidElementId, the default conduit type from the document will be used.
    /// </param>
    /// <param name="startPoint">The start point of the conduit location line.</param>
    /// <param name="endPoint">The end point of the conduit location line.</param>
    /// <param name="level">
    ///    The element of the level which this conduit based.
    ///    If the input level id is invalidElementId = -1, the nearest level will be used.
    /// </param>
    /// <returns>The newly created conduit.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    This conduit type is invalid.
    ///    -or-
    ///    This level id is invalid.
    ///    -or-
    ///    The points of startPoint and endPoint are too close: for MEPCurve, the minimum length is 1/10 inch.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///    A non-optional argument was null
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.DisabledDisciplineException">
    ///    None of the following disciplines is enabled: Mechanical Electrical Piping.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    The document is in failure mode: an operation has failed,
    ///    and Revit requires the user to either cancel the operation
    ///    or fix the problem (usually by deleting certain elements).
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
    ///    The document is in failure mode: an operation has failed,
    ///    and Revit requires the user to either cancel the operation
    ///    or fix the problem (usually by deleting certain elements).
    ///    -or-
    ///    The document is being loaded, or is in the midst of another
    ///    sensitive process.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
    ///    The document has no open transaction.
    /// </exception>
    [NodeCategory("Create")]
    public static Revit.Elements.Element? Create(Revit.Elements.Element conduitType,Autodesk.DesignScript.Geometry.Point startPoint,Autodesk.DesignScript.Geometry.Point endPoint,Revit.Elements.Element level)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        TransactionManager.Instance.ForceCloseTransaction();
        TransactionManager.Instance.EnsureInTransaction(doc);
        Autodesk.Revit.DB.Electrical.Conduit conduit = Autodesk.Revit.DB.Electrical.Conduit.Create(doc, new ElementId(conduitType.Id), startPoint.ToRevitType(),
            endPoint.ToRevitType(), new ElementId(level.Id));
        TransactionManager.Instance.TransactionTaskDone();
        return conduit.ToDynamoType();
    }
    
    /// <summary>
    /// Create a conduit by line
    /// </summary>
    /// <param name="conduitType">
    ///    The conduit type.  This must be a conduit type accepted by isValidConduitType().
    ///    If the input conduit type is InvalidElementId, the default conduit type from the document will be used.
    /// </param>
    /// <param name="line">the line define to draw conduit from start point to end point</param>
    /// <param name="level">the element of level</param>
    ///<returns name="conduit">new conduit</returns>
    [NodeCategory("Create")]
    public static Revit.Elements.Element? Create(Revit.Elements.Element conduitType,Autodesk.DesignScript.Geometry.Line line,Revit.Elements.Element level)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        TransactionManager.Instance.ForceCloseTransaction();
        TransactionManager.Instance.EnsureInTransaction(doc);
        Autodesk.Revit.DB.Electrical.Conduit conduit = Autodesk.Revit.DB.Electrical.Conduit.Create(doc, new ElementId(conduitType.Id), line.StartPoint.ToRevitType(),
            line.EndPoint.ToRevitType(), new ElementId(level.Id));
        TransactionManager.Instance.TransactionTaskDone();
        return conduit.ToDynamoType();
    }
}