﻿using Autodesk.Revit.DB;
using Dynamo.Graph.Nodes;
using Revit.GeometryConversion;
using RevitServices.Persistence;
using RevitServices.Transactions;

namespace OpenMEP.Element;

public class Duct
{
    private Duct()
    {
    }
    /// <summary>Creates a new duct that connects to two connectors.</summary>
    /// <remarks>
    ///    The new duct will have the same diameter and system type as the start connector. The creation will also connect the new duct
    ///    to two component who owns the specified connectors. If necessary, additional fitting(s) are included to make a valid connection.
    ///    If the new duct can not be connected to the next component (e.g., mismatched direction, no valid fitting, and etc), the new duct
    ///    will still be created at the specified connector position, and an InvalidOperationException is thrown.
    /// </remarks>
    /// <param name="ductType">The Element of the new duct type.</param>
    /// <param name="level">The level Element for the new duct.</param>
    /// <param name="startConnector">The first connector where the new duct starts.</param>
    /// <param name="endConnector">The second point of the new duct.</param>
    /// <returns>The created duct.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The duct type ductTypeId is not valid duct type.
    ///    -or-
    ///    The ElementId levelId is not a Level.
    ///    -or-
    ///    The connector's domain is not Domain.â€‹DomainHvac.
    ///    -or-
    ///    The points of startConnector and endConnector are too close: for MEPCurve, the minimum length is 1/10 inch.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///    A non-optional argument was null
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.DisabledDisciplineException">
    ///    None of the following disciplines is enabled: Mechanical Electrical Piping.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Thrown when the new duct fails to connect with the connector.
    /// </exception>
    /// <since>2017</since>
    [NodeCategory("Create")]
    public static void Create(global::Revit.Elements.Element ductType, global::Revit.Elements.Element level,
        Autodesk.Revit.DB.Connector startConnector, Autodesk.Revit.DB.Connector endConnector)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        TransactionManager.Instance.EnsureInTransaction(doc);
        Autodesk.Revit.DB.Mechanical.Duct.Create(doc, new ElementId(ductType.Id), new ElementId(level.Id),
            startConnector, endConnector);
        TransactionManager.Instance.TransactionTaskDone();
    }
    
    /// <summary>Creates a new duct that connects to the connector.</summary>
    /// <remarks>
    ///    The new duct will have the same diameter and system type as the specified connector. The creation will also connect the new duct
    ///    to the component who owns the specified connector. If necessary, additional fitting(s) are included to make a valid connection.
    ///    If the new duct can not be connected to the next component (e.g., mismatched direction, no valid fitting, and etc), the new duct
    ///    will still be created at the specified connector position, and an InvalidOperationException is thrown.
    /// </remarks>
    /// <param name="ductType">The Element of the new duct type.</param>
    /// <param name="level">The level for the new duct.</param>
    /// <param name="startConnector">The first connector where the new duct starts.</param>
    /// <param name="endPoint">The second point of the new duct.</param>
    /// <returns>The created duct.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The duct type ductTypeId is not valid duct type.
    ///    -or-
    ///    The ElementId levelId is not a Level.
    ///    -or-
    ///    The connector's domain is not Domain.â€‹DomainHvac.
    ///    -or-
    ///    The points of startConnector and endPoint are too close: for MEPCurve, the minimum length is 1/10 inch.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///    A non-optional argument was null
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.DisabledDisciplineException">
    ///    None of the following disciplines is enabled: Mechanical Electrical Piping.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Thrown when the new duct fails to connect with the connector.
    /// </exception>
    /// <since>2017</since>
    [NodeCategory("Create")]
    public static void Create(global::Revit.Elements.Element ductType, global::Revit.Elements.Element level,
        Autodesk.Revit.DB.Connector startConnector, Autodesk.DesignScript.Geometry.Point endPoint)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        TransactionManager.Instance.EnsureInTransaction(doc);
        Autodesk.Revit.DB.Mechanical.Duct.Create(doc, new ElementId(ductType.Id), new ElementId(level.Id),
            startConnector, endPoint.ToXyz());
        TransactionManager.Instance.TransactionTaskDone();
    }
    
    /// <summary>Creates a new duct from two points.</summary>
    /// <param name="systemType">The element of the HVAC system type.</param>
    /// <param name="ductType">The element of the duct type.</param>
    /// <param name="level">The level for the duct.</param>
    /// <param name="startPoint">The start point of the duct.</param>
    /// <param name="endPoint">The end point of the duct.</param>
    /// <returns>The created duct.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The systemType is not valid HVAC system type.
    ///    -or-
    ///    The duct type ductType is not valid duct type.
    ///    -or-
    ///    The Element level is not a Level.
    ///    -or-
    ///    The points of startPoint and endPoint are too close: for MEPCurve, the minimum length is 1/10 inch.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///    A non-optional argument was null
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.DisabledDisciplineException">
    ///    None of the following disciplines is enabled: Mechanical Electrical Piping.
    /// </exception>
    /// <since>2014</since>
    [NodeCategory("Create")]
    public static void Create(global::Revit.Elements.Element systemType, global::Revit.Elements.Element ductType, global::Revit.Elements.Element level,
        Autodesk.DesignScript.Geometry.Point startPoint, Autodesk.DesignScript.Geometry.Point endPoint)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        TransactionManager.Instance.EnsureInTransaction(doc);
        Autodesk.Revit.DB.Mechanical.Duct.Create(doc,new ElementId(systemType.Id), new ElementId(ductType.Id), new ElementId(level.Id),
            startPoint.ToXyz(), endPoint.ToXyz());
        TransactionManager.Instance.TransactionTaskDone();
    }
    
    /// <summary>Creates a new placeholder duct.</summary>
    /// <param name="systemType">The element of the HVAC system type.</param>
    /// <param name="ductType">The element of the duct type.</param>
    /// <param name="level">The element level for the duct.</param>
    /// <param name="startPoint">The first point of the placeholder line.</param>
    /// <param name="endPoint">The second point of the placeholder line.</param>
    /// <returns>The created placeholder duct.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The systemType is not valid HVAC system type.
    ///    -or-
    ///    The ductType is not valid duct type.
    ///    -or-
    ///    The Element level is not a Level.
    ///    -or-
    ///    The points of startPoint and endPoint are too close: for MEPCurve, the minimum length is 1/10 inch.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///    A non-optional argument was null
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.DisabledDisciplineException">
    ///    None of the following disciplines is enabled: Mechanical Electrical Piping.
    /// </exception>
    /// <since>2014</since>
    [NodeCategory("Create")]
    public static void CreatePlaceholder(global::Revit.Elements.Element systemType, global::Revit.Elements.Element ductType, global::Revit.Elements.Element level,
        Autodesk.DesignScript.Geometry.Point startPoint, Autodesk.DesignScript.Geometry.Point endPoint)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        TransactionManager.Instance.EnsureInTransaction(doc);
        Autodesk.Revit.DB.Mechanical.Duct.CreatePlaceholder(doc,new ElementId(systemType.Id), new ElementId(ductType.Id), new ElementId(level.Id),
            startPoint.ToXyz(), endPoint.ToXyz());
        TransactionManager.Instance.TransactionTaskDone();
    }

    /// <summary>Updates the associated system type for the duct.</summary>
    /// <remarks>
    ///    If the duct previously did not have a system associated to it, this will create a new system.
    /// </remarks>
    /// <param name="systemType">The Element of the hvac system type.</param>
    /// <param name="duct">The Element of the duct</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The systemTypeId is not valid HVAC system type.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///    A non-optional argument was null
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.DisabledDisciplineException">
    ///    None of the following disciplines is enabled: Mechanical Electrical Piping.
    /// </exception>
    /// <since>2017</since>
    /// <returns name="duct">duct changed systemType</returns>
    [NodeCategory("Action")]
    public static global::Revit.Elements.Element SetSystemType(global::Revit.Elements.Element duct,global::Revit.Elements.Element systemType)
    {
        Autodesk.Revit.DB.Mechanical.Duct? ductInternalElement = duct.InternalElement as Autodesk.Revit.DB.Mechanical.Duct;
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        TransactionManager.Instance.EnsureInTransaction(doc);
        ductInternalElement!.SetSystemType(new ElementId(systemType.Id));
        TransactionManager.Instance.TransactionTaskDone();
        return duct;
    }

    /// <summary>
    ///  Check if the element of duct is a valid system type
    /// </summary>
    /// <param name="systemType">the element of system type</param>
    /// <returns name="bool">true if is HvacSystemTypeId</returns>
    [NodeCategory("Query")]
    public static bool IsHvacSystemTypeId(global::Revit.Elements.Element systemType)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        bool isHvacSystemTypeId = Autodesk.Revit.DB.Mechanical.Duct.IsHvacSystemTypeId(doc, new ElementId(systemType.Id));
        return isHvacSystemTypeId;
    }
    /// <summary>
    /// Check if the element of duct is a valid duct type
    /// </summary>
    /// <param name="systemType">the system type of duct</param>
    /// <returns name="bool">true if is duct type id</returns>
    [NodeCategory("Query")]
    public static bool IsDuctTypeId(global::Revit.Elements.Element systemType)
    {
        Autodesk.Revit.DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
        return Autodesk.Revit.DB.Mechanical.Duct.IsDuctTypeId(doc, new ElementId(systemType.Id));
    }
}